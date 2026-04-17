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
            // 1. Start with all projects from the database
            var projectsQuery = _context.ProjectProposals.AsQueryable();

            // 2. Apply Faculty Filter
            if (!string.IsNullOrEmpty(faculty) && faculty != "All")
            {
                projectsQuery = projectsQuery.Where(p => p.Faculty == faculty);
            }

            // 3. Apply Subject Filter (Optional for now)
            if (!string.IsNullOrEmpty(subject))
            {
                projectsQuery = projectsQuery.Where(p => p.Subject.Contains(subject));
            }

            // 4. Apply Sorting
            if (sortBy == "name")
            {
                projectsQuery = projectsQuery.OrderBy(p => p.Title);
            }
            else
            {
                projectsQuery = projectsQuery.OrderByDescending(p => p.DateCreated);
            }

            // 5. Convert to list and send to the View
            var projectsList = projectsQuery.ToList();
            return View(projectsList);
        }

        // --- MANAGE ACCOUNTS ---
        public IActionResult ManageAccounts(string role = "Student")
        {
            // Fetch users based on the role (Student or Supervisor)
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
    }
}