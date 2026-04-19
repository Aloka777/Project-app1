using Microsoft.AspNetCore.Mvc;
using Projectapp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http; 

namespace Projectapp.Controllers
{
    public class StudentController : Controller
    {
        private static List<ProjectViewModel> _projects = new List<ProjectViewModel>
        {
            new ProjectViewModel { Title = "Housing meter", Group = "Group", Category = "IOT", Status = "Pending", Description = "cascmnksnvkndljkbnsdm...", ResearchArea = "IOT", TechnicalStack = "Embedded" },
            new ProjectViewModel { Title = "Monitoring system", Group = "Individual", Category = "IOT", Status = "Matched", Description = "cascmnksnvkndljkbnsdm...", ResearchArea = "AI", TechnicalStack = "Python" }
        };

        public IActionResult Index(string type, string sort)
        {
            var filteredProjects = _projects.AsEnumerable();
            if (!string.IsNullOrEmpty(type)) filteredProjects = filteredProjects.Where(p => p.Group == type);
            if (sort == "A to Z") filteredProjects = filteredProjects.OrderBy(p => p.Title);
            return View(filteredProjects.ToList());
        }

        [HttpGet]
        public IActionResult CreateProject() => View();

        [HttpPost]
        public IActionResult CreateProject(ProjectViewModel model, IFormFile proposalFile)
        {
            if (proposalFile != null && proposalFile.Length > 0)
            {
                model.ProposalFileName = proposalFile.FileName;
            }

            model.Status = "Pending";
            _projects.Add(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ProjectDetails(string title)
        {
            var project = _projects.FirstOrDefault(p => p.Title == title);
            if (project == null) return RedirectToAction("Index");
            return View(project);
        }

        [HttpPost]
        public IActionResult ProjectDetails(ProjectViewModel updatedModel, string originalTitle, IFormFile proposalFile)
        {
            var project = _projects.FirstOrDefault(p => p.Title == originalTitle);
            if (project != null)
            {
                project.Title = updatedModel.Title;
                project.ResearchArea = updatedModel.ResearchArea;
                project.TechnicalStack = updatedModel.TechnicalStack;
                project.Description = updatedModel.Description;
                project.Group = updatedModel.Group;

                if (proposalFile != null && proposalFile.Length > 0)
                {
                    project.ProposalFileName = proposalFile.FileName;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult WithdrawProject(string title)
        {
            var project = _projects.FirstOrDefault(p => p.Title == title);
            if (project != null)
            {
                project.Status = "Withdrawn";
            }
            return RedirectToAction("Index");
        }
    }
}