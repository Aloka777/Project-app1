using Microsoft.AspNetCore.Mvc;
using Projectapp.Models;
using System.Collections.Generic;

namespace Projectapp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            
            var projects = new List<ProjectViewModel>
            {
                new ProjectViewModel { Title = "Housing meter", Group = "69", Category = "IOT", Status = "Pending", Description = "cascmnksnvkndljkbnsdm..." },
                new ProjectViewModel { Title = "Monitoring system", Group = "individual", Category = "IOT", Status = "Matched", Description = "cascmnksnvkndljkbnsdm..." }
            };
            return View(projects);
        }

        public IActionResult CreateProject() => View();
        public IActionResult ProjectDetails() => View();
    }
}