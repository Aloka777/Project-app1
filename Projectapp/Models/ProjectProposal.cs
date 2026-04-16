using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ProjectProposal
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Abstract { get; set; } = string.Empty;

        [Required]
        public string TechnicalStack { get; set; } = string.Empty;

        [Required]
        public string ResearchArea { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        // Adding the '?' makes these nullable (they can be empty)
        public string? StudentName { get; set; }
        public string? SupervisorName { get; set; }
    }
}