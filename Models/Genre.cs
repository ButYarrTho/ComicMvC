using System.ComponentModel.DataAnnotations;

namespace ComicMvC.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(100)]
        public string GenreName { get; set; }

        // Navigation property: A genre can be associated with many comics
        public ICollection<Comic> Comics { get; set; }
    }
}