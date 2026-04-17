using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Student"; // Admin, Student, or Supervisor

        // Student Specific Fields
        public string? IndexNumber { get; set; }
        public string? Batch { get; set; }

        // Supervisor Specific Fields
        public string? AcademicId { get; set; }
        public string? Faculty { get; set; }

        // Shared Field
        public string? Degree { get; set; }
    }
}