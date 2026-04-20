using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Point 1: Hardcoded Admin Security
            if (email == "aloka@gmail.com" && password == "123")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            // Normal User Authentication
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                if (user.Role == "Student") return RedirectToAction("Dashboard", "Student");
                if (user.Role == "Supervisor") return RedirectToAction("Dashboard", "Supervisor");
            }

            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View();
        }
    }
}