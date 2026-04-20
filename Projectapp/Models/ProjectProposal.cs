using System;

namespace Projectapp.Models
{
    public class ProjectProposal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechnicalStack { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // --- NEW FIELDS TO FIX THE ERRORS ---
        public string GroupName { get; set; } // Fixes GroupName error
        public string Category { get; set; }  // Fixes Category error

        // Existing relationships
        public string Faculty { get; set; }
        public int StudentId { get; set; }
        public int? SupervisorId { get; set; }
        public string Status { get; set; } // e.g., "Matched", "Unmatched"
    }
}