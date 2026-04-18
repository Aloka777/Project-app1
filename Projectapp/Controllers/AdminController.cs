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

        // --- MANAGE KEYWORDS (LOAD PAGE WITH FILTER) ---
        public IActionResult ManageKeywords(string faculty = "IOT")
        {
            var keywords = _context.Keywords
                                   .Where(k => k.Faculty == faculty)
                                   .ToList();

            ViewBag.Faculty = faculty;
            return View(keywords);
        }

        // --- ADD KEYWORD ---
        [HttpPost]
        public IActionResult AddKeyword(string name, string faculty)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newKeyword = new Keyword
                {
                    Name = name,
                    Faculty = faculty
                };

                _context.Keywords.Add(newKeyword);
                _context.SaveChanges();
            }

            return RedirectToAction("ManageKeywords", new { faculty = faculty });
        }

        // --- DELETE KEYWORD ---
        public IActionResult DeleteKeyword(int id)
        {
            var keyword = _context.Keywords.Find(id);

            if (keyword != null)
            {
                string faculty = keyword.Faculty;

                _context.Keywords.Remove(keyword);
                _context.SaveChanges();

                return RedirectToAction("ManageKeywords", new { faculty = faculty });
            }

            return RedirectToAction("ManageKeywords");
        }
    }
}