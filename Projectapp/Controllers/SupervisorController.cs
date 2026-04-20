using Microsoft.AspNetCore.Mvc;

namespace Projectapp.Controllers
{
    public class SupervisorController : Controller
    {

        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpPost]
        public IActionResult FilterProjects(string sortBy, string area, string type)
        {

            return RedirectToAction("Dashboard");
        }


        public IActionResult AddToInterests(int id)
        {

            return RedirectToAction("Dashboard");
        }

        public IActionResult Interests()
        {
            return View();
        }

        public IActionResult ProjectDetails(int id)
        {
            return View();
        }

        public IActionResult MyAccount()
        {
            return View();
        }


    }
}