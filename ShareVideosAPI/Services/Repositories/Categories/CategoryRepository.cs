using ShareVideosAPI.Services.Entities;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.Services.Repositories.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContextPostgre context) : base(context)
        {
        }
    }
}
