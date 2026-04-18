using Microsoft.AspNetCore.Mvc;
using Projectapp.Data;
using Projectapp.Models;
using System.Collections.Generic;
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

        // --- DASHBOARD (PROJECT CARDS) ---
        public IActionResult Dashboard(string faculty, string subject, string sortBy)
        {
            var projectsQuery = _context.ProjectProposals.AsQueryable();

            if (!string.IsNullOrEmpty(faculty) && faculty != "All")
            {
                projectsQuery = projectsQuery.Where(p => p.Faculty == faculty);
            }

            if (!string.IsNullOrEmpty(subject))
            {
                projectsQuery = projectsQuery.Where(p => p.Subject.Contains(subject));
            }

            if (sortBy == "name")
            {
                projectsQuery = projectsQuery.OrderBy(p => p.Title);
            }
            else
            {
                projectsQuery = projectsQuery.OrderByDescending(p => p.DateCreated);
            }

            var projectsList = projectsQuery.ToList();
            return View(projectsList);
        }

        // --- MANAGE ACCOUNTS ---
        public IActionResult ManageAccounts(string role = "Student")
        {
            var users = _context.Users
                                .Where(u => u.Role == role)
                                .ToList() ?? new List<ApplicationUser>();

            ViewBag.SelectedRole = role;

            return View(users);
        }

        // --- CREATE ACCOUNT ---
        [HttpPost]
        public IActionResult CreateAccount(ApplicationUser newUser)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }

            return RedirectToAction("ManageAccounts", new { role = newUser.Role });
        }

        // --- MANAGE KEYWORDS ---
        public IActionResult ManageKeywords()
        {
            return View();
        }
    }
}