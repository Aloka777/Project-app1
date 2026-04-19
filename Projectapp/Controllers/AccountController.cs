using Microsoft.AspNetCore.Mvc;
using Projectapp.Data;
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
            // 1. Admin Hardcoded Check
            if (email == "aloka@gmail.com" && password == "123")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            // 2. Database Check for Student/Supervisor
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                if (user.Role == "Supervisor")
                {
                    // Redirects to SupervisorController -> Dashboard action
                    return RedirectToAction("Dashboard", "Supervisor");
                }
                else if (user.Role == "Student")
                {
                    // Redirects to StudentController -> Index action
                    return RedirectToAction("Index", "Student");
                }

                return RedirectToAction("Index", "Home");
            }

            // 3. Error Message if login fails
            ViewBag.ErrorMessage = "Invalid credentials. Please check your email and password.";
            return View();
        }
    }
}