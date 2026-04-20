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
            if (email == "aloka@gmail.com" && password == "123")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                if (user.Role == "Student")
                {
                    return RedirectToAction("Index", "Student", new { email = user.Email });
                }

                if (user.Role == "Supervisor")
                {
                    // FIXED: Changed "Index" to "Dashboard" to match SupervisorController
                    return RedirectToAction("Dashboard", "Supervisor", new { email = user.Email });
                }
            }

            ViewBag.ErrorMessage = "Invalid email or password. Please try again.";
            return View();
        }
    }
}