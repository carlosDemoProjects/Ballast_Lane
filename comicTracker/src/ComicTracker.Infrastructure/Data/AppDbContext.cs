using ComicTracker.Domain.Entities;
using ComicTracker.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Comic> Comics => Set<Comic>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComicConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
