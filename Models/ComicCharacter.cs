using System.ComponentModel.DataAnnotations.Schema;

namespace ComicMvC.Models
{
    public class ComicCharacter
    {
        public int ComicId { get; set; }
        public int CharacterId { get; set; }

        [ForeignKey("ComicId")]
        public Comic Comic { get; set; }

        [ForeignKey("CharacterId")]
        public Character Character { get; set; }
    }
}