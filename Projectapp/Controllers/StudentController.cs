using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projectapp.Data;
using Projectapp.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projectapp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string email)
        {
            var student = _context.Users.FirstOrDefault(u => u.Email == email && u.Role == "Student");
            if (student == null) return RedirectToAction("Login", "Account");

            var projects = _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Where(p => p.StudentId == student.Id).ToList();

            ViewBag.Student = student;
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.ResearchAreas = _context.ResearchAreas
                .Where(ra => ra.Faculty.Name == student.Faculty).ToList();
            ViewBag.Keywords = _context.MasterKeywords.ToList();

            return View(projects);
        }

        [HttpPost]
        public async Task<IActionResult> ManageProject(ProjectProposal model, IFormFile? proposalFile, string[] memberIds, int[] selectedKeywords, string ProjectType)
        {
            model.ProjectType = ProjectType;
            model.Type = ProjectType;

            if (model.ProjectType == "Group")
            {
                if (memberIds != null && memberIds.Length > 0)
                {
                    model.TeamMembers = string.Join(", ", memberIds.Where(id => !string.IsNullOrEmpty(id)));
                }
            }
            else
            {
                model.TeamMembers = "Individual";
                model.GroupName = null;
            }

            var studentUser = _context.Users.Find(model.StudentId);
            if (studentUser != null)
            {
                model.Faculty = studentUser.Faculty;
            }

            if (selectedKeywords == null || selectedKeywords.Length == 0)
            {
                ModelState.AddModelError("Keywords", "Keywords are mandatory.");
                return RedirectToAction("Index", new { email = studentUser?.Email });
            }

            if (proposalFile != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(proposalFile.FileName);
                string path = Path.Combine(folder, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await proposalFile.CopyToAsync(stream);
                }
                model.ProposalFilePath = "/uploads/" + fileName;
            }

            model.DateCreated = DateTime.Now;
            model.Status = "Pending";

            _context.ProjectProposals.Add(model);
            await _context.SaveChangesAsync();

            foreach (var kwId in selectedKeywords)
            {
                _context.ProjectKeywords.Add(new ProjectKeyword { ProjectId = model.Id, KeywordId = kwId });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { email = studentUser?.Email });
        }

        // --- UPDATED PROFILE METHOD WITH ADDRESS, BDAY, EMAIL, PASSWORD ---
        [HttpPost]
        public IActionResult UpdateProfile(ApplicationUser updatedData)
        {
            var user = _context.Users.Find(updatedData.Id);
            if (user != null)
            {
                // Core Info
                user.FullName = updatedData.FullName;
                user.Email = updatedData.Email; // Fix: Saves Email

                // Identity Info
                user.NIC = updatedData.NIC;
                user.IndexNumber = updatedData.IndexNumber;

                // Missing Info Fixes:
                user.Address = updatedData.Address; // Fix: Saves Address
                user.Birthday = updatedData.Birthday; // Fix: Saves Birthday

                // Academic Info
                user.Faculty = updatedData.Faculty;
                user.Batch = updatedData.Batch;
                user.Degree = updatedData.Degree;

                // Password Security Fix:
                // Only update if the user actually typed something in the password box
                if (!string.IsNullOrEmpty(updatedData.PasswordHash))
                {
                    user.PasswordHash = updatedData.PasswordHash;
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { email = user?.Email });
        }
    }
}