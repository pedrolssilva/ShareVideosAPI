using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShareVideosAPI.Middlewares.Filters;
using ShareVideosAPI.Models;
using ShareVideosAPI.Models.Video;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace ShareVideosAPI.Controllers
{
    [ApiController]
    [Route("videos")]
    [Produces("application/json")]
    public class VideosController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public VideosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(summary: " List Videos", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return a list of videos", typeof(List<VideoModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Index()
        {
            var videos = _unitOfWork.VideoRepository.List();
            var result = _mapper.Map<List<Video>, List<VideoModel>>(videos);
            return Ok(result);
        }

        [HttpPost]
        [ValidateModelStateCustom]
        [SwaggerOperation(summary: " Create a new video", description: "")]
        [SwaggerResponse(StatusCodes.Status201Created, "Return created video", typeof(VideoModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Fields required", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Create(CreateVideoInput createVideo)
        {
            var video = _unitOfWork.VideoRepository.Insert(new Video
            {
                Title = createVideo.Title,
                Description = createVideo.Description,
                Url = createVideo.Url
            });

            if(video is null)
            {
                return StatusCode(500, "Something went wrong in creation of the video.");
            }

            var result = _mapper.Map<Video, VideoModel>(video);
            return Ok(result);
        }
    }
}
