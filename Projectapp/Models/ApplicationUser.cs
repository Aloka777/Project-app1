using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Student"; // Student or Supervisor

        public string? IndexNumber { get; set; }
        public string? NIC { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Faculty { get; set; }

        // --- FIXED: Added missing properties ---
        public string? Batch { get; set; }
        public string? Degree { get; set; }

        // Supervisor Specific
        public string? AcademicId { get; set; }
        public string? Expertise { get; set; }
        public string? ContactNumber { get; set; }
        public string? Keywords { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}