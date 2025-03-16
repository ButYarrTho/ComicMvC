using System.ComponentModel.DataAnnotations.Schema;

namespace ComicMvC.Models
{
    public class ComicCreator
    {
        public int ComicId { get; set; }
        public int CreatorId { get; set; }

        [ForeignKey("ComicId")]
        public Comic Comic { get; set; }

        [ForeignKey("CreatorId")]
        public Creator Creator { get; set; }
    }
}
