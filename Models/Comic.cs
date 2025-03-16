using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComicMvC.Models
{
    public class Comic
    {
        [Key]
        public int ComicId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int IssueNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        [StringLength(500)]
        public string CoverImageUrl { get; set; }

        public string Synopsis { get; set; }

        // Navigation properties for many-to-many relationships
        public ICollection<ComicCharacter> ComicCharacters { get; set; }
        public ICollection<ComicCreator> ComicCreators { get; set; }
    }
}