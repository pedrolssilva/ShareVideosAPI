using Microsoft.EntityFrameworkCore;
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

        public Video? Update(int id, string? title, string? description, string? url)
        {
            Video? foundVideo = base.GetByKey(id);
            if (foundVideo == null)
            {
                return null;
            }

            foundVideo.Title = !string.IsNullOrEmpty(title) ? title : foundVideo.Title;
            foundVideo.Description = !string.IsNullOrEmpty(description) ? description : foundVideo.Description;
            foundVideo.Url = !string.IsNullOrEmpty(url) ? url : foundVideo.Url;

            base.Update(foundVideo);

            return foundVideo;
        }

        public IEnumerable<Video>? GetByNameSearch(string search)
        {
            var videos = _context.Videos
                            .Where(video => EF.Functions.Like(video.Title!, $"%{search}%"))
                            .ToList();
            return videos;
        }
       
    }
}
