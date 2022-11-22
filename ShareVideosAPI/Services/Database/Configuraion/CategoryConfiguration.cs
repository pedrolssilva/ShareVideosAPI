using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPI.Services.Database.Configuraion
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .ToTable("categories")
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(c => c.Color)
                .IsFixedLength(true)
                .HasMaxLength(7)
                .IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("CHK_Category_ColorHas7Chars", "(length(\"Color\") > 6)"));

            builder
                .HasMany(c => c.Videos)
                .WithOne(v => v.Category);
        }
    }
}
