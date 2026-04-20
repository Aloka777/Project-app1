using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projectapp.Models
{
    public class MasterKeyword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ResearchAreaId { get; set; }

        [ForeignKey("ResearchAreaId")]
        public virtual ResearchArea? ResearchArea { get; set; }
    }
}