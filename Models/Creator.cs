using System.ComponentModel.DataAnnotations;

namespace ComicMvC.Models
{
    public class Creator
    {
        [Key]
        public int CreatorId { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Role { get; set; }

        // Navigation property for many-to-many relationship with Comics
    }
}
