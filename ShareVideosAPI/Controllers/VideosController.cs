﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShareVideosAPI.Middlewares.Filters;
using ShareVideosAPI.Models;
using ShareVideosAPI.Models.Video;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(summary: " Get Video by Id", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return a video", typeof(VideoModel))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Get(int id)
        {
            var video = _unitOfWork.VideoRepository.GetByKey(id);
            if (video is null)
            {
                return NotFound(new { error = "Video not found" });
            }

            var result = _mapper.Map<Video, VideoModel>(video);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetByName")]
        [SwaggerOperation(summary: "Get videos by searching name matched", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return a list of matched searched videos", typeof(IEnumerable<VideoModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Video not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult GetByName([Required] string search)
        {
            var video = _unitOfWork.VideoRepository.GetByNameSearch(search);
            if (video is null)
            {
                return NotFound(new { error = "Any result was found for this search" });
            }

            var result = _mapper.Map<IEnumerable<Video>, IEnumerable<VideoModel>>(video);
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

            if (video is null)
            {
                return StatusCode(500, "Something went wrong in creation of the video.");
            }

            var result = _mapper.Map<Video, VideoModel>(video);
            return Ok(result);
        }


        [HttpPut]
        [Route("{id}")]
        [ValidateModelStateCustom]
        [SwaggerOperation(summary: " Update a video", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return updated video", typeof(VideoModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Fields required", type: typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Video not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Update(int id, UpdateVideoInput videoInput)
        {
            var video = _unitOfWork.VideoRepository.Update(
                id, videoInput.Title, videoInput.Description, videoInput.Url);

            if (video is null)
            {
                return NotFound(new { error = "Video not found" });
            }

            var result = _mapper.Map<Video, VideoModel>(video);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ValidateModelStateCustom]
        [SwaggerOperation(summary: " Delete a video", description: "")]
        [SwaggerResponse(StatusCodes.Status200OK, "Return deleted video", typeof(VideoModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Video not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal ServerError")]
        public IActionResult Delete(int id)
        {
            var video = _unitOfWork.VideoRepository.Delete(id);

            if (video is null)
            {
                return NotFound(new { error = "Video not found" });
            }

            var result = _mapper.Map<Video, VideoModel>(video);
            return Ok(result);
        }
    }
}
