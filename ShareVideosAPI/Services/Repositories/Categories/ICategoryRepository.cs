using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPI.Services.Repositories.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Category? Update(int id, string? title, string? color);

        ICollection<Video>? GetVideosByCategoryId(int id);
    }
}
