using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ProjectProposal
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        // This fixes the 'Description' error
        public string Description { get; set; } = string.Empty;

        // This fixes the 'GroupName' error
        public string GroupName { get; set; } = string.Empty;

        // This fixes the 'Category' error
        public string Category { get; set; } = string.Empty;

        public string Status { get; set; } = "Unmatched";

        // These fix the 'Faculty' and 'Subject' errors
        public string Faculty { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

        // This fixes the 'DateCreated' error
        public DateTime DateCreated { get; set; } = DateTime.Now;

       

      

        [Required]
        public string Abstract { get; set; } = string.Empty;

        [Required]
        public string TechnicalStack { get; set; } = string.Empty;

        [Required]
        public string ResearchArea { get; set; } = string.Empty;

    

        // Adding the '?' makes these nullable (they can be empty)
        public string? StudentName { get; set; }
        public string? SupervisorName { get; set; }
    }
}