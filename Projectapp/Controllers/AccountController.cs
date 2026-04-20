using Microsoft.AspNetCore.Mvc;
using Projectapp.Data;
using Projectapp.Models;
using System.Linq;

namespace Projectapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // 1. Hardcoded Admin Access
            // This bypasses the database entirely for the admin
            if (email == "aloka@gmail.com" && password == "123")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            // 2. Student & Supervisor Login
            // We search the 'Users' table for a match
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                // Redirect based on the Role column in your database
                if (user.Role == "Student")
                {
                    return RedirectToAction("Index", "Student", new { email = user.Email });
                }

                if (user.Role == "Supervisor")
                {
                    return RedirectToAction("Index", "Supervisor", new { email = user.Email });
                }
            }

            // 3. Error Handling
            ViewBag.ErrorMessage = "Invalid email or password. Please try again.";
            return View();
        }
    }
}