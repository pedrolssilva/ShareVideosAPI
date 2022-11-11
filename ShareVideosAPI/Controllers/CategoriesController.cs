using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareVideosAPI.Middlewares.Filters;
using ShareVideosAPI.Models.Video;
using ShareVideosAPI.Models;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Entities;
using Swashbuckle.AspNetCore.Annotations;
using ShareVideosAPI.Models.Category;

namespace ShareVideosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModelStateCustom]
        [SwaggerOperation(summary: " Create a new category", description: "")]
        [SwaggerResponse(StatusCodes.Status201Created, "Return created category", typeof(CategoryModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Fields required", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Create(CreateCategoryInput createCategory)
        {
            var category = _unitOfWork.CategoryRepository.Insert(new Category
            {
                Title = createCategory.Title,
                Color = createCategory.Color
            });

            if (category is null)
            {
                return StatusCode(500, "Something went wrong in creation of the video.");
            }

            var result = _mapper.Map<Category, CategoryModel>(category);
            return Ok(result);
        }
    }
}
