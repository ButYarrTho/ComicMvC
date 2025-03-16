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
                if (context.Comics.Any())
                {
                    return;
                }

                // --------------------------
                // Publishers
                // --------------------------
                context.Publishers.AddRange(
                    new Publisher { PublisherId = 1, Name = "Marvel Comics", FoundedYear = 1939, Headquarters = "New York" },
                    new Publisher { PublisherId = 2, Name = "DC Comics", FoundedYear = 1934, Headquarters = "Burbank" },
                    new Publisher { PublisherId = 3, Name = "Image Comics", FoundedYear = 1992, Headquarters = "Portland" },
                    new Publisher { PublisherId = 4, Name = "Dark Horse Comics", FoundedYear = 1986, Headquarters = "Milwaukie" },
                    new Publisher { PublisherId = 5, Name = "IDW Publishing", FoundedYear = 1999, Headquarters = "San Diego" }
                );

                // --------------------------
                // Genres
                // --------------------------
                context.Genres.AddRange(
                    new Genre { GenreId = 1, GenreName = "Action" },
                    new Genre { GenreId = 2, GenreName = "Sci-Fi" },
                    new Genre { GenreId = 3, GenreName = "Horror" },
                    new Genre { GenreId = 4, GenreName = "Fantasy" },
                    new Genre { GenreId = 5, GenreName = "Mystery" }
                );

                // --------------------------
                // Creators
                // --------------------------
                context.Creators.AddRange(
                    new Creator { CreatorId = 1, FullName = "Stan Lee", Role = "Writer" },
                    new Creator { CreatorId = 2, FullName = "Jack Kirby", Role = "Artist" },
                    new Creator { CreatorId = 3, FullName = "Frank Miller", Role = "Writer/Artist" },
                    new Creator { CreatorId = 4, FullName = "Alan Moore", Role = "Writer" },
                    new Creator { CreatorId = 5, FullName = "Neil Gaiman", Role = "Writer" },
                    new Creator { CreatorId = 6, FullName = "Jim Lee", Role = "Artist" },
                    new Creator { CreatorId = 7, FullName = "George Pérez", Role = "Artist" },
                    new Creator { CreatorId = 8, FullName = "Grant Morrison", Role = "Writer" },
                    new Creator { CreatorId = 9, FullName = "Todd McFarlane", Role = "Artist" },
                    new Creator { CreatorId = 10, FullName = "Brian Michael Bendis", Role = "Writer" }
                );

                // --------------------------
                // Comics
                // --------------------------
                context.Comics.AddRange(
                    new Comic
                    {
                        ComicId = 1,
                        Title = "Spider-Man: Homecoming",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(2017, 7, 7),
                        PublisherId = 1,
                        GenreId = 1,
                        CoverImageUrl = "https://example.com/spiderman1.jpg",
                        Synopsis = "Peter Parker balances high school life with his responsibilities as Spider-Man."
                    },
                    new Comic
                    {
                        ComicId = 2,
                        Title = "Spider-Man: Far From Home",
                        IssueNumber = 2,
                        ReleaseDate = new DateTime(2019, 7, 2),
                        PublisherId = 1,
                        GenreId = 1,
                        CoverImageUrl = "https://example.com/spiderman2.jpg",
                        Synopsis = "Spider-Man faces new villains during a school trip abroad."
                    },
                    new Comic
                    {
                        ComicId = 3,
                        Title = "Batman: Year One",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(1987, 2, 1),
                        PublisherId = 2,
                        GenreId = 5,
                        CoverImageUrl = "https://example.com/batman1.jpg",
                        Synopsis = "The origin story of Gotham's dark knight."
                    },
                    new Comic
                    {
                        ComicId = 4,
                        Title = "Watchmen",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(1986, 9, 1),
                        PublisherId = 2,
                        GenreId = 3,
                        CoverImageUrl = "https://example.com/watchmen.jpg",
                        Synopsis = "A gritty deconstruction of the superhero genre."
                    },
                    new Comic
                    {
                        ComicId = 5,
                        Title = "Saga",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(2012, 3, 14),
                        PublisherId = 3,
                        GenreId = 4,
                        CoverImageUrl = "https://example.com/saga1.jpg",
                        Synopsis = "An epic space opera that follows star-crossed lovers from warring species."
                    },
                    new Comic
                    {
                        ComicId = 6,
                        Title = "Hellboy: Seed of Destruction",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(1994, 8, 15),
                        PublisherId = 4,
                        GenreId = 3,
                        CoverImageUrl = "https://example.com/hellboy1.jpg",
                        Synopsis = "Hellboy battles supernatural creatures as he discovers his own origins."
                    },
                    new Comic
                    {
                        ComicId = 7,
                        Title = "Locke & Key",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(2008, 2, 20),
                        PublisherId = 5,
                        GenreId = 5,
                        CoverImageUrl = "https://example.com/locke1.jpg",
                        Synopsis = "A family moves into a house filled with mysterious keys that unlock powers."
                    },
                    new Comic
                    {
                        ComicId = 8,
                        Title = "X-Men: Days of Future Past",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(1981, 1, 1),
                        PublisherId = 1,
                        GenreId = 2,
                        CoverImageUrl = "https://example.com/xmen1.jpg",
                        Synopsis = "Mutants fight for their survival in a dystopian future."
                    },
                    new Comic
                    {
                        ComicId = 9,
                        Title = "The Sandman: Preludes & Nocturnes",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(1989, 11, 29),
                        PublisherId = 2,
                        GenreId = 4,
                        CoverImageUrl = "https://example.com/sandman.jpg",
                        Synopsis = "Dream and his siblings navigate the realms of the waking and the dream."
                    },
                    new Comic
                    {
                        ComicId = 10,
                        Title = "Invincible",
                        IssueNumber = 1,
                        ReleaseDate = new DateTime(2003, 1, 1),
                        PublisherId = 3,
                        GenreId = 1,
                        CoverImageUrl = "https://example.com/invincible1.jpg",
                        Synopsis = "A young superhero learns about his powers and faces dark challenges."
                    }
                );

                // --------------------------
                // Characters
                // --------------------------
                context.Characters.AddRange(
                    new Character
                    {
                        CharacterId = 1,
                        Name = "Spider-Man",
                        Alias = "Peter Parker",
                        Description = "A high school student with spider-like abilities.",
                        FirstAppearanceComicId = 1
                    },
                    new Character
                    {
                        CharacterId = 2,
                        Name = "Mary Jane Watson",
                        Alias = "MJ",
                        Description = "Peter Parker's love interest and a strong, independent woman.",
                        FirstAppearanceComicId = 1
                    },
                    new Character
                    {
                        CharacterId = 3,
                        Name = "Batman",
                        Alias = "Bruce Wayne",
                        Description = "A billionaire who fights crime as the Dark Knight.",
                        FirstAppearanceComicId = 3
                    },
                    new Character
                    {
                        CharacterId = 4,
                        Name = "The Joker",
                        Alias = "",
                        Description = "Batman's chaotic archenemy with a twisted sense of humor.",
                        FirstAppearanceComicId = 3
                    },
                    new Character
                    {
                        CharacterId = 5,
                        Name = "Rorschach",
                        Alias = "",
                        Description = "A masked vigilante with an unyielding moral code.",
                        FirstAppearanceComicId = 4
                    },
                    new Character
                    {
                        CharacterId = 6,
                        Name = "Alana",
                        Alias = "",
                        Description = "One of the protagonists in Saga, caught between warring factions.",
                        FirstAppearanceComicId = 5
                    },
                    new Character
                    {
                        CharacterId = 7,
                        Name = "Hellboy",
                        Alias = "",
                        Description = "A demon summoned from Hell who fights supernatural forces.",
                        FirstAppearanceComicId = 6
                    },
                    new Character
                    {
                        CharacterId = 8,
                        Name = "Liz Sherman",
                        Alias = "",
                        Description = "A pyrokinetic with a tragic past.",
                        FirstAppearanceComicId = 6
                    },
                    new Character
                    {
                        CharacterId = 9,
                        Name = "Kinsey",
                        Alias = "",
                        Description = "A mysterious character involved in the secrets of the Locke house.",
                        FirstAppearanceComicId = 7
                    },
                    new Character
                    {
                        CharacterId = 10,
                        Name = "Cyclops",
                        Alias = "",
                        Description = "Leader of the X-Men, able to emit powerful optic blasts.",
                        FirstAppearanceComicId = 8
                    }
                );

                // relationships between comics and characters.
                context.Set<ComicCharacter>().AddRange(
                    new ComicCharacter { ComicId = 1, CharacterId = 1 },
                    new ComicCharacter { ComicId = 1, CharacterId = 2 },
                    new ComicCharacter { ComicId = 2, CharacterId = 1 },
                    new ComicCharacter { ComicId = 3, CharacterId = 3 },
                    new ComicCharacter { ComicId = 3, CharacterId = 4 },
                    new ComicCharacter { ComicId = 4, CharacterId = 5 },
                    new ComicCharacter { ComicId = 5, CharacterId = 6 },
                    new ComicCharacter { ComicId = 6, CharacterId = 7 },
                    new ComicCharacter { ComicId = 6, CharacterId = 8 },
                    new ComicCharacter { ComicId = 7, CharacterId = 9 },
                    new ComicCharacter { ComicId = 8, CharacterId = 10 },
                    new ComicCharacter { ComicId = 9, CharacterId = 5 },
                    new ComicCharacter { ComicId = 10, CharacterId = 1 }
                );

                
                //relationships between comics and creators.
                context.Set<ComicCreator>().AddRange(
                    new ComicCreator { ComicId = 1, CreatorId = 1 },
                    new ComicCreator { ComicId = 1, CreatorId = 2 },
                    new ComicCreator { ComicId = 2, CreatorId = 1 },
                    new ComicCreator { ComicId = 3, CreatorId = 3 },
                    new ComicCreator { ComicId = 3, CreatorId = 7 },
                    new ComicCreator { ComicId = 4, CreatorId = 4 },
                    new ComicCreator { ComicId = 5, CreatorId = 5 },
                    new ComicCreator { ComicId = 6, CreatorId = 9 },
                    new ComicCreator { ComicId = 7, CreatorId = 8 },
                    new ComicCreator { ComicId = 8, CreatorId = 10 },
                    new ComicCreator { ComicId = 9, CreatorId = 4 },
                    new ComicCreator { ComicId = 10, CreatorId = 1 },
                    new ComicCreator { ComicId = 10, CreatorId = 6 }
                );

                // Users
                context.Users.AddRange(
                    new User { UserId = 1, Username = "admin", Email = "admin@example.com", PasswordHash = "hashedpassword", Role = "Admin" },
                    new User { UserId = 2, Username = "editor", Email = "editor@example.com", PasswordHash = "hashedpassword", Role = "Editor" }
                );

                // Save all seeded data to the database.
                context.SaveChanges();
            }
        }
    }
}
