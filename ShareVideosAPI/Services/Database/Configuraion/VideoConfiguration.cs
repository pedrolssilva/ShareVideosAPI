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
        }
    }
}
