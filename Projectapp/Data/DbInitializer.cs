using Projectapp.Models;
using System.Linq;

namespace Projectapp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if faculties already exist to avoid duplicates
            if (context.Faculties.Any()) return;

            // 1. Define Faculties
            var foc = new Faculty { Name = "FOC" };
            var fob = new Faculty { Name = "FOB" };
            var fos = new Faculty { Name = "FOS" };
            var foe = new Faculty { Name = "FOE" };

            context.Faculties.AddRange(foc, fob, fos, foe);
            context.SaveChanges();

            // 2. Define Research Areas Linked to Faculties
            var researchAreas = new[]
            {
                // FOC
                new ResearchArea { Name = "Information Technology (Cyber Security)", FacultyId = foc.Id },
                new ResearchArea { Name = "Technology Management", FacultyId = foc.Id },
                new ResearchArea { Name = "Computer Science", FacultyId = foc.Id },
                new ResearchArea { Name = "Computer Networks", FacultyId = foc.Id },
                new ResearchArea { Name = "Computer Security", FacultyId = foc.Id },
                new ResearchArea { Name = "Software Engineering", FacultyId = foc.Id },
                new ResearchArea { Name = "Data Science", FacultyId = foc.Id },
                new ResearchArea { Name = "Artificial Intelligence", FacultyId = foc.Id },

                // FOB
                new ResearchArea { Name = "Business Analytics", FacultyId = fob.Id },
                new ResearchArea { Name = "Tourism, Hospitality & Events", FacultyId = fob.Id },
                new ResearchArea { Name = "Multimedia", FacultyId = fob.Id },
                new ResearchArea { Name = "Accounting and Finance", FacultyId = fob.Id },
                new ResearchArea { Name = "Project Management", FacultyId = fob.Id },
                new ResearchArea { Name = "Human Resource Management", FacultyId = fob.Id },
                new ResearchArea { Name = "International Business", FacultyId = fob.Id },

                // FOS
                new ResearchArea { Name = "Psychology", FacultyId = fos.Id },
                new ResearchArea { Name = "Biomedical Science", FacultyId = fos.Id },

                // FOE
                new ResearchArea { Name = "Mechatronic Engineering", FacultyId = foe.Id },
                new ResearchArea { Name = "Electrical & Electronic Engineering", FacultyId = foe.Id },
                new ResearchArea { Name = "Computer Systems Engineering", FacultyId = foe.Id },
                new ResearchArea { Name = "Interior Design", FacultyId = foe.Id }
            };

            context.ResearchAreas.AddRange(researchAreas);
            context.SaveChanges();
        }
    }
}