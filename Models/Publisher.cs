using System.ComponentModel.DataAnnotations;

namespace ComicMvC.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int FoundedYear { get; set; }

        [StringLength(255)]
        public string Headquarters { get; set; }

        // Navigation property: A publisher can have many comics
        public ICollection<Comic> Comics { get; set; }
    }
}
