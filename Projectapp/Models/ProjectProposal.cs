using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ProjectProposal
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        
        public string Description { get; set; } = string.Empty;

        
        public string GroupName { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Status { get; set; } = "Unmatched";

        
        public string Faculty { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

       
        public DateTime DateCreated { get; set; } = DateTime.Now;

       

      

        [Required]
        public string Abstract { get; set; } = string.Empty;

        [Required]
        public string TechnicalStack { get; set; } = string.Empty;

        [Required]
        public string ResearchArea { get; set; } = string.Empty;

    

        
        public string? StudentName { get; set; }
        public string? SupervisorName { get; set; }
    }
}