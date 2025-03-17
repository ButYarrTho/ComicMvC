using ComicMvC.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicMvC.Data
{
    public class ComicsContext : DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options)
            : base(options)
        {
        }

        public DbSet<Comic> Comics { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        // Removed: ComicCharacters and ComicCreators

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Remove configuration for ComicCharacter and ComicCreator

            // For the Character → Comic reference (FirstAppearanceComicId):
            modelBuilder.Entity<Character>()
                .HasOne(ch => ch.FirstAppearanceComic)
                .WithMany() // No navigation property on Comic for this relationship.
                .HasForeignKey(ch => ch.FirstAppearanceComicId)
                .OnDelete(DeleteBehavior.Cascade);  // Or choose the appropriate delete behavior

            // Other configurations for your entities remain as needed.
        }
    }
}
