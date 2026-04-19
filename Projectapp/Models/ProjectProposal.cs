using System;
using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ProjectProposal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? ProjectType { get; set; }

        public string? Category { get; set; }

        public string? ResearchArea { get; set; }

        public string? TechnicalStack { get; set; }

        [Required]
        public string Abstract { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public string? ProposalFileName { get; set; } 
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Description { get; set; } = string.Empty;

        public string GroupName { get; set; } = string.Empty;

        public string Faculty { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

        public string? StudentName { get; set; }
        public string? SupervisorName { get; set; }
    }
}