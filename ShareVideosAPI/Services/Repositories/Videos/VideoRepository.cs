using ShareVideosAPI.Services.Entities;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.Services.Repositories.Videos
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        public VideoRepository(DbContextPostgre context) : base(context)
        {
        }
    }
}
