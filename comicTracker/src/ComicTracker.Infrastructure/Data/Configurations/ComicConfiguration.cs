using ComicTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicTracker.Infrastructure.Data.Configurations
{
    public class ComicConfiguration : IEntityTypeConfiguration<Comic>
    {
        public void Configure(EntityTypeBuilder<Comic> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Writer).IsRequired().HasMaxLength(150);            
            builder.Property(c => c.Artist).HasMaxLength(80);
            builder.Property(c => c.Publisher).HasMaxLength(100);
            builder.Property(c => c.Readed).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
        }
    }
}
