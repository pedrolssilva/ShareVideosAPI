using ShareVideosAPI.Services.Entities;
using ShareVideosAPIatabase;
using System;

namespace ShareVideosAPI.Services.Repositories.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContextPostgre context) : base(context)
        {
        }

        public Category? Update(int id, string? title, string? color)
        {
            Category? categoryFound = base.GetByKey(id);
            if (categoryFound == null)
            {
                return null;
            }

            categoryFound.Title = !string.IsNullOrEmpty(title) ? title : categoryFound.Title;
            categoryFound.Color = !string.IsNullOrEmpty(color) ? color : categoryFound.Color;
            base.Update(categoryFound);

            return categoryFound;
        }
    }
}
