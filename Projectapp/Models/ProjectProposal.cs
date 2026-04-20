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

        public string? GroupName { get; set; }

        // This is the field the controller will now use
        public string ProjectType { get; set; }

        // Keeping 'Type' for backward compatibility if needed, 
        // but we will primarily use ProjectType
        public string? Type { get; set; }

        public string? TeamMembers { get; set; }

        public string Faculty { get; set; }

        public int ResearchAreaId { get; set; }

        [ForeignKey("ResearchAreaId")]
        public virtual ResearchArea? ResearchArea { get; set; }

        [NotMapped]
        public string Category => ResearchArea?.Name ?? "General";

        public string TechnicalStack { get; set; }

        public string Abstract { get; set; }

        public string? ProposalFilePath { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public string StudentId { get; set; }

        public string? SupervisorId { get; set; }
    }
}