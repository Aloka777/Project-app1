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
        public StudentController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index(string email)
        {
            var student = _context.Users.FirstOrDefault(u => u.Email == email && u.Role == "Student");
            if (student == null) return RedirectToAction("Login", "Account");

            var projects = _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Where(p => p.StudentId == student.Id).ToList();

            ViewBag.Student = student;
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.ResearchAreas = _context.ResearchAreas.Where(ra => ra.Faculty.Name == student.Faculty).ToList();
            ViewBag.Keywords = _context.MasterKeywords.ToList();

            return View(projects);
        }

        [HttpPost]
        public IActionResult UpdateProfile(ApplicationUser updatedData)
        {
            var user = _context.Users.Find(updatedData.Id);
            if (user != null)
            {
                user.FullName = updatedData.FullName;
                user.Faculty = updatedData.Faculty;
                user.Batch = updatedData.Batch;
                user.Degree = updatedData.Degree;
                user.Address = updatedData.Address;
                user.Gender = updatedData.Gender;
                user.Birthday = updatedData.Birthday;
                user.NIC = updatedData.NIC;
                user.IndexNumber = updatedData.IndexNumber;
                user.Email = updatedData.Email;
                if (!string.IsNullOrEmpty(updatedData.PasswordHash)) user.PasswordHash = updatedData.PasswordHash;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { email = user?.Email });
        }

        [HttpPost]
        public async Task<IActionResult> ManageProject(ProjectProposal model, IFormFile? proposalFile, string[] memberIds)
        {
            ProjectProposal project;
            if (model.Id == 0)
            {
                project = model;
                project.DateCreated = DateTime.Now;
                project.Status = "Pending";
                _context.ProjectProposals.Add(project);
            }
            else
            {
                project = _context.ProjectProposals.Find(model.Id);
                project.Title = model.Title;
                project.ResearchAreaId = model.ResearchAreaId;
                project.TechnicalStack = model.TechnicalStack;
                project.Abstract = model.Abstract;
            }

            if (memberIds != null) project.TeamMembers = string.Join(",", memberIds);

            if (proposalFile != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", proposalFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create)) { await proposalFile.CopyToAsync(stream); }
                project.ProposalFilePath = "/uploads/" + proposalFile.FileName;
            }

            await _context.SaveChangesAsync();
            var user = _context.Users.Find(project.StudentId);
            return RedirectToAction("Index", new { email = user?.Email });
        }
    }
}