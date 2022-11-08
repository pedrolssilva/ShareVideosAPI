using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPI.Services.Repositories.Videos
{
    public interface IVideoRepository : IBaseRepository<Video>
    {
        Video? Update(int id, string? title, string? description, string? url);
    }
}
