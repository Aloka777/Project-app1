using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projectapp.Models
{
    public class SupervisorInterest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SupervisorId { get; set; } = string.Empty;

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string Status { get; set; } = "Interested";

        [ForeignKey("ProjectId")]
        public virtual ProjectProposal? Project { get; set; }
    }
}