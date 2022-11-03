using ShareVideosAPI.Services.Entities;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.Services.Repositories.Videos
{
    /// <summary>
    /// VideoRepository to access database table
    /// </summary>
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public VideoRepository(DbContextPostgre context) : base(context)
        {
        }
    }
}
