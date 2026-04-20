using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projectapp.Data;
using Projectapp.Models;
using System.Linq;

namespace Projectapp.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- DASHBOARD: Smart Filtering Logic ---
        public async Task<IActionResult> Dashboard(string email, string sortBy, string typeFilter)
        {
            var supervisor = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (supervisor == null) return RedirectToAction("Login", "Account");

            ViewBag.Supervisor = supervisor;
            ViewBag.Faculties = await _context.Faculties.ToListAsync();
            ViewBag.MasterKeywords = await _context.MasterKeywords.ToListAsync();

            // Logic: Get projects matching user keywords
            var userKeywords = (supervisor.Keywords ?? "").Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrEmpty(k)).ToList();
            var query = _context.ProjectProposals.AsQueryable();

            if (userKeywords.Any())
            {
                // Smart Search: Check if title or abstract contains ANY of the supervisor's keywords
                query = query.Where(p => userKeywords.Any(uk => p.Title.Contains(uk) || p.Abstract.Contains(uk)));
            }

            // Manual Filters
            if (!string.IsNullOrEmpty(typeFilter)) query = query.Where(p => p.ProjectType == typeFilter);
            if (sortBy == "a-z") query = query.OrderBy(p => p.Title);
            else if (sortBy == "newest") query = query.OrderByDescending(p => p.Id);

            return View(await query.ToListAsync());
        }

        // --- PROFILE: Save account details to relevant tables ---
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ApplicationUser updatedData, string[] selectedKeywords)
        {
            var user = await _context.Users.FindAsync(updatedData.Id);
            if (user != null)
            {
                user.FullName = updatedData.FullName;
                user.Faculty = updatedData.Faculty;
                user.AcademicId = updatedData.AcademicId;
                user.NIC = updatedData.NIC;
                user.Gender = updatedData.Gender;
                user.ContactNumber = updatedData.ContactNumber;
                user.Keywords = selectedKeywords != null ? string.Join(",", selectedKeywords) : "";

                if (!string.IsNullOrEmpty(updatedData.PasswordHash))
                    user.PasswordHash = updatedData.PasswordHash;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard", new { email = user?.Email });
        }

        // --- INTERESTS: Add project to supervisor list ---
        [HttpPost]
        public async Task<IActionResult> AddToInterests(int projectId, string supervisorEmail)
        {
            var supervisor = await _context.Users.FirstOrDefaultAsync(u => u.Email == supervisorEmail);
            if (supervisor != null)
            {
                var exists = await _context.SupervisorInterests.AnyAsync(i => i.ProjectId == projectId && i.SupervisorId == supervisor.Id);
                if (!exists)
                {
                    _context.SupervisorInterests.Add(new SupervisorInterest
                    {
                        ProjectId = projectId,
                        SupervisorId = supervisor.Id,
                        Status = "Interested"
                    });
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Dashboard", new { email = supervisorEmail });
        }

        public async Task<IActionResult> Interests(string email)
        {
            var supervisor = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (supervisor == null) return RedirectToAction("Login", "Account");

            var interests = await _context.SupervisorInterests
                .Include(i => i.Project)
                .Where(i => i.SupervisorId == supervisor.Id)
                .ToListAsync();

            ViewBag.Supervisor = supervisor;
            return View(interests);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessInterest(int interestId, string action)
        {
            var interest = await _context.SupervisorInterests.Include(i => i.Project).FirstOrDefaultAsync(i => i.Id == interestId);
            if (interest != null)
            {
                var supervisor = await _context.Users.FindAsync(interest.SupervisorId);
                if (action == "Accept")
                {
                    interest.Status = "Accepted";
                    if (interest.Project != null)
                    {
                        interest.Project.SupervisorId = interest.SupervisorId;
                        interest.Project.Status = "Accepted";
                    }
                }
                else if (action == "Remove")
                {
                    _context.SupervisorInterests.Remove(interest);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Interests", new { email = supervisor?.Email });
            }
            return RedirectToAction("Login", "Account");
        }
    }
}