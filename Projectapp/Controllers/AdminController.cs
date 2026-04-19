using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Dashboard(string faculty, string type, string subject)
        {
            var projectsQuery = _context.ProjectProposals.AsQueryable();

            if (!string.IsNullOrEmpty(faculty) && faculty != "All")
            {
                projectsQuery = projectsQuery.Where(p => p.Faculty == faculty);
            }

            if (!string.IsNullOrEmpty(subject))
            {
                projectsQuery = projectsQuery.Where(p => p.Subject == subject);
            }

            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                projectsQuery = projectsQuery.Where(p => p.Category == type);
            }

            var projectsList = projectsQuery.OrderByDescending(p => p.DateCreated).ToList();
            return View(projectsList);
        }

        public IActionResult ManageAccounts(string role = "Student")
        {
            var users = _context.Users.Where(u => u.Role == role).ToList();
            ViewBag.SelectedRole = role;
            return View(users);
        }

        // --- NEW CREATE ACCOUNT METHOD ---
        [HttpPost]
        public IActionResult CreateAccount(ApplicationUser newUser)
        {
            if (newUser != null)
            {
                // Identity framework requires a UserName. We'll use the Email.
                newUser.UserName = newUser.Email;

                // Ensure FullName is not null to satisfy your [Required] attribute
                if (string.IsNullOrEmpty(newUser.FullName))
                {
                    newUser.FullName = "User"; // Fallback safety
                }

                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Redirect back to the list of the role just created
                return RedirectToAction("ManageAccounts", new { role = newUser.Role });
            }

            return RedirectToAction("ManageAccounts");
        }
    }
}