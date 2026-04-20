using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projectapp.Models
{
    public class ProjectProposal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? GroupName { get; set; } // For Group projects

        public string Type { get; set; } // Individual or Group

        public string? TeamMembers { get; set; } // Comma-separated Student IDs

        public string Faculty { get; set; }

        public int ResearchAreaId { get; set; }

        [ForeignKey("ResearchAreaId")]
        public virtual ResearchArea ResearchArea { get; set; }

        // Smart Category helper
        [NotMapped]
        public string Category => ResearchArea?.Name ?? "General";

        public string TechnicalStack { get; set; }

        public string Abstract { get; set; }

        public string? ProposalFilePath { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // Pending, Accepted

        public string StudentId { get; set; } // Linked to ApplicationUser.Id

        public string? SupervisorId { get; set; } // Hidden until Accepted
    }
}