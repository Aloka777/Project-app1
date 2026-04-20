using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Student";

        // Student Specific
        public string? IndexNumber { get; set; }
        public string? Batch { get; set; }

        // Supervisor Specific
        public string? AcademicId { get; set; }
        public string? Faculty { get; set; }

        // Shared
        public string? Degree { get; set; }

        [Required]
        public string PasswordHash { get; set; } // For simple 123 logic
    }
}