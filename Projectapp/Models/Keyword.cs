using System.ComponentModel.DataAnnotations;

namespace Projectapp.Models
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Faculty { get; set; }


    }
}