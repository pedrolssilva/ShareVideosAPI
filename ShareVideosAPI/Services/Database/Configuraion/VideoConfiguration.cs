using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPI.Services.Database.Configuraion
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder
                .ToTable("videos")
                .HasKey(x => x.Id);

            builder
                .Property(v => v.Title)
                .HasMaxLength(50);

            builder
                .HasOne(v => v.Category)
                .WithMany(c => c.Videos);

            builder.Property<int>("CategoryId")
                .HasDefaultValue(1);
        }
    }
}
