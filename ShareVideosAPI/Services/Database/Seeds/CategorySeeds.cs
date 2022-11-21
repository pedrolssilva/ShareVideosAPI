using ShareVideosAPI.Services.Entities;
using System.Security.Principal;

namespace ShareVideosAPI.Services.Database.Seeds
{
    public class CategorySeeds
    {
        private IUnitOfWork _unitOfWork;
        public Dictionary<string, Category> Seeds { get; }

        public CategorySeeds(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Seeds = new()
            {
                {
                    "FreeCategory",
                    new Category()
                    {
                       Id = 1,
                       Title =  "Livre",
                       Color = "#1b4d3e"
                    }
                }
            };
        }

        public void ApplySeed()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.List();
            foreach (Category seed in Seeds.Values)
            {
                Category? category = categories.Find(category => category.Id == seed.Id);
                if (category == null)
                {
                    _unitOfWork.CategoryRepository.Insert(seed);
                }
                else
                {
                    seed.Id = category.Id;
                }
            }
        }
    }
}
