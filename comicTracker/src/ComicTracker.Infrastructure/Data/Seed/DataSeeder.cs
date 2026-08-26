using ComicTracker.Domain.Entities;

namespace ComicTracker.Infrastructure.Data.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Users.Any()) return;

            var user = User.Create("Demo", "User", "demo@comictracker.com", BCrypt.Net.BCrypt.HashPassword("Demo1234!"));
            await context.Users.AddAsync(user);

            var comics = new[]
            {
                Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", user.Id, true),
                Comic.Create("Batman The Killing Joke", "Alan Moore", "Brian Booland", "DC Comics", user.Id),
                Comic.Create("Batman The Long Halloween", "Jeph Loeb", "Tim Sale", "DC Comics", user.Id, true),
                Comic.Create("Civil War", "Mark Millar", "Steve McNiven", "Marvel", user.Id),
                Comic.Create("House of M", "Bendis", "Coipel","Marvel", user.Id, true)
            };

            await context.Comics.AddRangeAsync(comics);
            await context.SaveChangesAsync();
        }
    }
}
    