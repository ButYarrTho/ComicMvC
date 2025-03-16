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
        public DbSet<ComicCharacter> ComicCharacters { get; set; }
        public DbSet<ComicCreator> ComicCreators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite primary key for ComicCharacter
            modelBuilder.Entity<ComicCharacter>()
                .HasKey(cc => new { cc.ComicId, cc.CharacterId });

            // Configure the foreign keys to use Restrict or NoAction instead of Cascade
            modelBuilder.Entity<ComicCharacter>()
                .HasOne(cc => cc.Comic)
                .WithMany(c => c.ComicCharacters)
                .HasForeignKey(cc => cc.ComicId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<ComicCharacter>()
                .HasOne(cc => cc.Character)
                .WithMany(ch => ch.ComicCharacters)
                .HasForeignKey(cc => cc.CharacterId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            // Composite primary key for ComicCreator
            modelBuilder.Entity<ComicCreator>()
                .HasKey(cc => new { cc.ComicId, cc.CreatorId });

            // If needed, also specify for ComicCreator
            modelBuilder.Entity<ComicCreator>()
                .HasOne(cc => cc.Comic)
                .WithMany(c => c.ComicCreators)
                .HasForeignKey(cc => cc.ComicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComicCreator>()
                .HasOne(cc => cc.Creator)
                .WithMany(c => c.ComicCreators)
                .HasForeignKey(cc => cc.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // For the Character → Comic reference (FirstAppearanceComicId):
            modelBuilder.Entity<Character>()
                .HasOne(ch => ch.FirstAppearanceComic)
                .WithMany() // or .WithMany(c => c.Characters) if you had a nav property
                .HasForeignKey(ch => ch.FirstAppearanceComicId)
                .OnDelete(DeleteBehavior.Restrict); // or NoAction

            // ...
        }
    }
}