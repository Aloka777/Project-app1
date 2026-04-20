using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // FIX: Ensure this is virtual to allow Entity Framework to load data into it
        public virtual ICollection<ResearchArea> ResearchAreas { get; set; } = new List<ResearchArea>();
    }
}