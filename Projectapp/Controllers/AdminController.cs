using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projectapp.Data;
using Projectapp.Models;
using System.Linq;

namespace Projectapp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard: Handles Points 2, 4, 5
        public IActionResult Dashboard(string sortBy, string facultyFilter, string status)
        {
            // FIX: Added .Include(f => f.ResearchAreas) to ensure the sidebar can see the areas
            ViewBag.Faculties = _context.Faculties
                                        .Include(f => f.ResearchAreas)
                                        .ToList();

            var projects = _context.ProjectProposals.AsQueryable();

            // Point 5: Matched/Unmatched Logic
            if (status == "Matched")
                projects = projects.Where(p => p.SupervisorId != null);
            else if (status == "Unmatched")
                projects = projects.Where(p => p.SupervisorId == null);

            // Point 4: Smart Filtering
            if (!string.IsNullOrEmpty(facultyFilter) && facultyFilter != "All")
                projects = projects.Where(p => p.Faculty == facultyFilter);

            // Point 4: Sorting
            projects = sortBy switch
            {
                "name" => projects.OrderBy(p => p.Title),
                "faculty" => projects.OrderBy(p => p.Faculty),
                _ => projects.OrderByDescending(p => p.DateCreated)
            };

            return View(projects.ToList());
        }

        // Point 3: AJAX Dynamic Research Areas
        [HttpGet]
        public IActionResult GetResearchAreas(int facultyId)
        {
            var areas = _context.ResearchAreas
                .Where(a => a.FacultyId == facultyId)
                .Select(a => new { id = a.Id, name = a.Name })
                .ToList();
            return Json(areas);
        }

        // Point 3: Keyword Storage
        [HttpPost]
        public IActionResult AddKeyword(int researchAreaId, string keywordName)
        {
            if (!string.IsNullOrEmpty(keywordName))
            {
                var keyword = new MasterKeyword
                {
                    Name = keywordName,
                    ResearchAreaId = researchAreaId
                };
                _context.MasterKeywords.Add(keyword);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        // Point 6 & 7: Account Management
        public IActionResult ManageAccounts(string searchId, string facultyFilter, string role = "Student")
        {
            ViewBag.Faculties = _context.Faculties.ToList();
            var users = _context.Users.Where(u => u.Role == role).AsQueryable();

            if (!string.IsNullOrEmpty(searchId))
                users = users.Where(u => u.IndexNumber == searchId || u.AcademicId == searchId);

            if (!string.IsNullOrEmpty(facultyFilter) && facultyFilter != "All")
                users = users.Where(u => u.Faculty == facultyFilter);

            return View(users.ToList());
        }

        // Point 8: Create Account Logic
        [HttpPost]
        public IActionResult CreateAccount(ApplicationUser user)
        {
            user.UserName = user.Email;
            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedUserName = user.Email.ToUpper();

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("ManageAccounts", new { role = user.Role });
        }
    }
}