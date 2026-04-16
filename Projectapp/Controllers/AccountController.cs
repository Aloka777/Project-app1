using Microsoft.AspNetCore.Mvc;

namespace Projectapp.Controllers
{
    public class AccountController : Controller
    {
        // --- LOGIN SECTION ---

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            // Hardcoded credentials for Student, Supervisor, and Admin
            if (email == "aloka@gmail.com" && password == "123")
            {
                // Successful login redirects to the Home page
                return RedirectToAction("Index", "Home");
            }

            // If login fails
            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }

        // --- FORGET PASSWORD SECTION ---

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(string email, string role)
        {
            // For now, we simulate sending an email
            if (!string.IsNullOrEmpty(email))
            {
                ViewBag.Message = $"A reset link has been sent to {email} as a {role}.";
            }
            else
            {
                ViewBag.Error = "Please enter a valid email address.";
            }

            return View();
        }
    }
}