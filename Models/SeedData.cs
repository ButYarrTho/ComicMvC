using System;
using System.Linq;
using ComicMvC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComicMvC.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ComicsContext(
                serviceProvider.GetRequiredService<DbContextOptions<ComicsContext>>()))
            {
                // If any comics exist, assume the database has been seeded.
                if (context.Comics.Any())
                {
                    return;
                }

                // --------------------------
                // Publishers
                // --------------------------
                var publisher1 = new Publisher { Name = "Marvel Comics", FoundedYear = 1939, Headquarters = "New York" };
                var publisher2 = new Publisher { Name = "DC Comics", FoundedYear = 1934, Headquarters = "Burbank" };
                var publisher3 = new Publisher { Name = "Image Comics", FoundedYear = 1992, Headquarters = "Portland" };
                var publisher4 = new Publisher { Name = "Dark Horse Comics", FoundedYear = 1986, Headquarters = "Milwaukie" };
                var publisher5 = new Publisher { Name = "IDW Publishing", FoundedYear = 1999, Headquarters = "San Diego" };

                context.Publishers.AddRange(publisher1, publisher2, publisher3, publisher4, publisher5);
                context.SaveChanges();

                // --------------------------
                // Genres
                // --------------------------
                var genre1 = new Genre { GenreName = "Action" };
                var genre2 = new Genre { GenreName = "Sci-Fi" };
                var genre3 = new Genre { GenreName = "Horror" };
                var genre4 = new Genre { GenreName = "Fantasy" };
                var genre5 = new Genre { GenreName = "Mystery" };

                context.Genres.AddRange(genre1, genre2, genre3, genre4, genre5);
                context.SaveChanges();

                // --------------------------
                // Creators
                // --------------------------
                var creator1 = new Creator { FullName = "Stan Lee", Role = "Writer" };
                var creator2 = new Creator { FullName = "Jack Kirby", Role = "Artist" };
                var creator3 = new Creator { FullName = "Frank Miller", Role = "Writer/Artist" };
                var creator4 = new Creator { FullName = "Alan Moore", Role = "Writer" };
                var creator5 = new Creator { FullName = "Neil Gaiman", Role = "Writer" };
                var creator6 = new Creator { FullName = "Jim Lee", Role = "Artist" };
                var creator7 = new Creator { FullName = "George Pérez", Role = "Artist" };
                var creator8 = new Creator { FullName = "Grant Morrison", Role = "Writer" };
                var creator9 = new Creator { FullName = "Todd McFarlane", Role = "Artist" };
                var creator10 = new Creator { FullName = "Brian Michael Bendis", Role = "Writer" };

                context.Creators.AddRange(creator1, creator2, creator3, creator4, creator5,
                                          creator6, creator7, creator8, creator9, creator10);
                context.SaveChanges();

                // --------------------------
                // Comics
                // --------------------------
                var comic1 = new Comic
                {
                    Title = "Spider-Man: Homecoming",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(2017, 7, 7),
                    PublisherId = publisher1.PublisherId,
                    GenreId = genre1.GenreId,
                    CoverImageUrl = "https://example.com/spiderman1.jpg",
                    Synopsis = "Peter Parker balances high school life with his responsibilities as Spider-Man."
                };
                var comic2 = new Comic
                {
                    Title = "Spider-Man: Far From Home",
                    IssueNumber = 2,
                    ReleaseDate = new DateTime(2019, 7, 2),
                    PublisherId = publisher1.PublisherId,
                    GenreId = genre1.GenreId,
                    CoverImageUrl = "https://example.com/spiderman2.jpg",
                    Synopsis = "Spider-Man faces new villains during a school trip abroad."
                };
                var comic3 = new Comic
                {
                    Title = "Batman: Year One",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(1987, 2, 1),
                    PublisherId = publisher2.PublisherId,
                    GenreId = genre5.GenreId,
                    CoverImageUrl = "https://example.com/batman1.jpg",
                    Synopsis = "The origin story of Gotham's dark knight."
                };
                var comic4 = new Comic
                {
                    Title = "Watchmen",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(1986, 9, 1),
                    PublisherId = publisher2.PublisherId,
                    GenreId = genre3.GenreId,
                    CoverImageUrl = "https://example.com/watchmen.jpg",
                    Synopsis = "A gritty deconstruction of the superhero genre."
                };
                var comic5 = new Comic
                {
                    Title = "Saga",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(2012, 3, 14),
                    PublisherId = publisher3.PublisherId,
                    GenreId = genre4.GenreId,
                    CoverImageUrl = "https://example.com/saga1.jpg",
                    Synopsis = "An epic space opera that follows star-crossed lovers from warring species."
                };
                var comic6 = new Comic
                {
                    Title = "Hellboy: Seed of Destruction",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(1994, 8, 15),
                    PublisherId = publisher4.PublisherId,
                    GenreId = genre3.GenreId,
                    CoverImageUrl = "https://example.com/hellboy1.jpg",
                    Synopsis = "Hellboy battles supernatural creatures as he discovers his own origins."
                };
                var comic7 = new Comic
                {
                    Title = "Locke & Key",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(2008, 2, 20),
                    PublisherId = publisher5.PublisherId,
                    GenreId = genre5.GenreId,
                    CoverImageUrl = "https://example.com/locke1.jpg",
                    Synopsis = "A family moves into a house filled with mysterious keys that unlock powers."
                };
                var comic8 = new Comic
                {
                    Title = "X-Men: Days of Future Past",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(1981, 1, 1),
                    PublisherId = publisher1.PublisherId,
                    GenreId = genre2.GenreId,
                    CoverImageUrl = "https://example.com/xmen1.jpg",
                    Synopsis = "Mutants fight for their survival in a dystopian future."
                };
                var comic9 = new Comic
                {
                    Title = "The Sandman: Preludes & Nocturnes",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(1989, 11, 29),
                    PublisherId = publisher2.PublisherId,
                    GenreId = genre4.GenreId,
                    CoverImageUrl = "https://example.com/sandman.jpg",
                    Synopsis = "Dream and his siblings navigate the realms of the waking and the dream."
                };
                var comic10 = new Comic
                {
                    Title = "Invincible",
                    IssueNumber = 1,
                    ReleaseDate = new DateTime(2003, 1, 1),
                    PublisherId = publisher3.PublisherId,
                    GenreId = genre1.GenreId,
                    CoverImageUrl = "https://example.com/invincible1.jpg",
                    Synopsis = "A young superhero learns about his powers and faces dark challenges."
                };

                context.Comics.AddRange(comic1, comic2, comic3, comic4, comic5,
                                        comic6, comic7, comic8, comic9, comic10);
                context.SaveChanges();

                // --------------------------
                // Characters
                // --------------------------
                var character1 = new Character
                {
                    Name = "Spider-Man",
                    Alias = "Peter Parker",
                    Description = "A high school student with spider-like abilities.",
                    FirstAppearanceComicId = comic1.ComicId
                };
                var character2 = new Character
                {
                    Name = "Mary Jane Watson",
                    Alias = "MJ",
                    Description = "Peter Parker's love interest and a strong, independent woman.",
                    FirstAppearanceComicId = comic1.ComicId
                };
                var character3 = new Character
                {
                    Name = "Batman",
                    Alias = "Bruce Wayne",
                    Description = "A billionaire who fights crime as the Dark Knight.",
                    FirstAppearanceComicId = comic3.ComicId
                };
                var character4 = new Character
                {
                    Name = "The Joker",
                    Alias = "",
                    Description = "Batman's chaotic archenemy with a twisted sense of humor.",
                    FirstAppearanceComicId = comic3.ComicId
                };
                var character5 = new Character
                {
                    Name = "Rorschach",
                    Alias = "",
                    Description = "A masked vigilante with an unyielding moral code.",
                    FirstAppearanceComicId = comic4.ComicId
                };
                var character6 = new Character
                {
                    Name = "Alana",
                    Alias = "",
                    Description = "One of the protagonists in Saga, caught between warring factions.",
                    FirstAppearanceComicId = comic5.ComicId
                };
                var character7 = new Character
                {
                    Name = "Hellboy",
                    Alias = "",
                    Description = "A demon summoned from Hell who fights supernatural forces.",
                    FirstAppearanceComicId = comic6.ComicId
                };
                var character8 = new Character
                {
                    Name = "Liz Sherman",
                    Alias = "",
                    Description = "A pyrokinetic with a tragic past.",
                    FirstAppearanceComicId = comic6.ComicId
                };
                var character9 = new Character
                {
                    Name = "Kinsey",
                    Alias = "",
                    Description = "A mysterious character involved in the secrets of the Locke house.",
                    FirstAppearanceComicId = comic7.ComicId
                };
                var character10 = new Character
                {
                    Name = "Cyclops",
                    Alias = "",
                    Description = "Leader of the X-Men, able to emit powerful optic blasts.",
                    FirstAppearanceComicId = comic8.ComicId
                };

                context.Characters.AddRange(character1, character2, character3, character4, character5,
                                            character6, character7, character8, character9, character10);
                context.SaveChanges();

                // --------------------------
                // Users
                // --------------------------
                context.Users.AddRange(
                    new User { Username = "admin", Email = "admin@example.com", PasswordHash = "hashedpassword", Role = "Admin" },
                    new User { Username = "editor", Email = "editor@example.com", PasswordHash = "hashedpassword", Role = "Editor" }
                );

                // Save all seeded data to the database.
                context.SaveChanges();
            }
        }
    }
}
