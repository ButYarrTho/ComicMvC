﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicMvC.Models
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        public string Description { get; set; }

        public int FirstAppearanceComicId { get; set; }

        [ForeignKey("FirstAppearanceComicId")]
        public Comic FirstAppearanceComic { get; set; }

        // Removed: Navigation property for many-to-many relationship with Comics.
        // public ICollection<ComicCharacter> ComicCharacters { get; set; }
    }
}
