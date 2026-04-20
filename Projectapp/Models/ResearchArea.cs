using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projectapp.Models
{
    public class ResearchArea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Foreign Key property
        public int FacultyId { get; set; }

        // FIX: Navigation property back to Faculty
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
    }
}