using AutoMapper;
using ShareVideosAPI.Models.Category;
using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPI.Services.Mapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
