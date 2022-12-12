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

        [HttpGet]
        [SwaggerOperation(summary: " List Categories", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return a list of Categories", typeof(List<CategoryModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Index()
        {
            var Categories = _unitOfWork.CategoryRepository.List();
            var result = _mapper.Map<List<Category>, List<CategoryModel>>(Categories);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(summary: " Get Category by Id", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return a category", typeof(CategoryModel))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Get(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetByKey(id);
            if (category is null)
            {
                return NotFound(new { error = "Category not found" });
            }

            var result = _mapper.Map<Category, CategoryModel>(category);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/videos")]
        [SwaggerOperation(summary: " Get videos by category Id", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return a list of videos", typeof(ICollection<VideoModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult GetVideoByCategoryId(int id)
        {
            var videos = _unitOfWork.CategoryRepository.GetVideosByCategoryId(id);
            if (videos is null)
            {
                return NotFound(new { error = "Category not found" });
            }

            var result = _mapper.Map<ICollection<Video>, ICollection<VideoModel>>(videos);
            return Ok(result);
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

        [HttpPut]
        [Route("{id}")]
        [ValidateModelStateCustom]
        [SwaggerOperation(summary: " Update a category", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return updated category", typeof(CategoryModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Fields required", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Update(int id, UpdateCategoryInput videoInput)
        {
            var category = _unitOfWork.CategoryRepository.Update(id, videoInput.Title, videoInput.Color);

            if (category is null)
            {
                return NotFound(new { error = "Category not found" });
            }

            var result = _mapper.Map<Category, CategoryModel>(category);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ValidateModelStateCustom]
        [SwaggerOperation(summary: " Delete a category", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return deleted category", typeof(CategoryModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Delete(int id)
        {
            var Category = _unitOfWork.CategoryRepository.Delete(id);

            if (Category is null)
            {
                return NotFound(new { error = "Category not found" });
            }

            var result = _mapper.Map<Category, CategoryModel>(Category);
            return Ok(result);
        }
    }
}
