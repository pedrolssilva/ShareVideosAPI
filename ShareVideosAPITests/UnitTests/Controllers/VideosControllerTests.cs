using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShareVideosAPI.Controllers;
using ShareVideosAPI.Models.Video;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Entities;
using ShareVideosAPI.Services.Mapper.Profiles;
using System.Net;

namespace ShareVideosAPITests.UnitTests.Controllers
{
    public class VideosControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = new();
        private Mock<Mapper> _mapperMock;
        private VideosController _target;

        public VideosControllerTests()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VideoProfile>();
            });
            _mapperMock = new Mock<Mapper>(mapperConfig);

            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = new IPAddress(0);
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            _target = new VideosController(_unitOfWorkMock.Object, _mapperMock.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Fact(DisplayName = "Index_WithDatabaseError_Should_ThrowDatabaseException")]
        [Trait("Video Index", "Tests behaviors")]
        public void Index_WithDatabaseError_Should_ThrowDatabaseException()
        {
            // Arrange
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.List())
                .Throws(new DatabaseException("Test exception", null))
                .Verifiable();

            // Act
            Func<IActionResult> result = () =>
            {
                return _target.Index();
            };

            // Assert
            result.Should().Throw<DatabaseException>();
        }

        [Fact(DisplayName = "Index__Should_Return_OK")]
        [Trait("Video Index", "Tests behaviors")]
        public void Index__Should_Return_OK()
        {
            // Arrange
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.List())
                .Returns(VideosControllerTestData.GetVideos().ToList())
                .Verifiable();

            // Act
            var result = _target.Index();

            // Assert
            result.As<OkObjectResult>().Value.Should().BeOfType<List<VideoModel>>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<OkObjectResult>().Value.As<List<VideoModel>>().Count.Should().Be(10);
        }

        [Fact(DisplayName = "Get_VideoNotFound_Should_Return_NotFound")]
        [Trait("Video Get", "Tests behaviors")]
        public void Get_VideoNotFound_Should_Return_NotFound()
        {
            // Arrange
            Video? video = null;
            int videoId = new Faker().Random.Int();
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.GetByKey())
                .Returns(video)
                .Verifiable();

            // Act

            var result = _target.Get(videoId);

            // Assert
            result.As<NotFoundObjectResult>().Value.Should().BeEquivalentTo(
                new
                {
                    error = "Video not found"
                });
            result.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact(DisplayName = "Get_Should_Return_OK")]
        [Trait("Video Get", "Tests behaviors")]
        public void Get_Should_Return_OK()
        {
            // Arrange
            int videoId = new Faker().Random.Int();
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.GetByKey(videoId))
                .Returns(() =>
                {
                    var video = VideosControllerTestData.GetVideos(1).First();
                    video.Id = videoId;
                    return video;
                })
                .Verifiable();

            // Act

            var result = _target.Get(videoId);

            // Assert
            result.As<OkObjectResult>().Value.Should().BeOfType<VideoModel>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<OkObjectResult>().Value.As<VideoModel>().Id.Should().Be(videoId);
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "GetByName_VideoNotFound_Should_Return_NotFound")]
        [Trait("Video GetByName", "Tests behaviors")]
        public void GetByName_VideoNotFound_Should_Return_NotFound()
        {
            // Arrange
            IList<Video>? videos = null;
            var videoNameToSeach = new Faker().Vehicle.Model();
            int videoId = new Faker().Random.Int();
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.GetByNameSearch(It.IsAny<string>()))
                .Returns(videos)
                .Verifiable();

            // Act
            var result = _target.GetByName(videoNameToSeach);

            // Assert
            result.As<NotFoundObjectResult>().Value.Should().BeEquivalentTo(
                new
                {
                    error = "Any result was found for this search"
                });
            result.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "GetByName_Should_Return_OK")]
        [Trait("Video GetByName", "Tests behaviors")]
        public void GetByName_Should_Return_OK()
        {
            // Arrange
            var videoNameToSeach = new Faker().Vehicle.Model();
            IList<Video>? videos = VideosControllerTestData.GetVideos();
            videos.First().Title = videoNameToSeach;
            videos.Last().Title = videoNameToSeach;
            int videoId = new Faker().Random.Int();
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.GetByNameSearch(It.IsAny<string>()))
                .Returns(() =>
                {
                    return videos.Where(video => video.Title.Contains(videoNameToSeach));
                })
                .Verifiable();

            // Act

            var result = _target.GetByName(videoNameToSeach);

            // Assert
            result.As<OkObjectResult>().Value.Should().BeOfType<List<VideoModel>>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<OkObjectResult>().Value.As<List<VideoModel>>().Count().Should().Be(2);
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "Create_WithInsertError_Should_Return_InternalServerError")]
        [Trait("Video Create", "Tests behaviors")]
        public void Create_WithInsertError_Should_Return_InternalServerError()
        {
            // Arrange
            var inputModel = VideosControllerTestData.GetCreateVideoInput();
            Video? video = null;
            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.Insert(It.IsAny<Video>()))
                .Returns(video)
                .Verifiable();

            // Act
            var result = _target.Create(inputModel);

            // Assert
            result.As<ObjectResult>().Value.Should().BeOfType<string>()
                .And.Be("Something went wrong in creation of the video.");
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "Create_WithInsertError_Should_Return_OK")]
        [Trait("Video Create", "Tests behaviors")]
        public void Create_WithInsertError_Should_Return_OK()
        {
            // Arrange
            var inputModel = VideosControllerTestData.GetCreateVideoInput();
            Video? video = new Faker<Video>()
                .Rules((faker, target) =>
                {
                    target.Id = faker.Random.Int();
                    target.Title = inputModel.Title;
                    target.Description = inputModel.Description;
                    target.Url = inputModel.Url;
                }).Generate();

            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.Insert(It.IsAny<Video>()))
                .Returns(video)
                .Verifiable();

            // Act
            var result = _target.Create(inputModel);

            // Assert
            result.As<OkObjectResult>().Value.Should().BeOfType<VideoModel>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            _unitOfWorkMock.VerifyAll();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(inputModel, options =>
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }

        [Fact(DisplayName = "Update_WithInvalidUserId_Should_Return_NotFound")]
        [Trait("Video update", "Tests behaviors")]
        public void Update_WithInvalidUserId_Should_Return_NotFound()
        {
            // Arrange
            var inputModel = VideosControllerTestData.GetUpdateVideoInput();
            Video? video = null;

            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.Update(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    )
                )
                .Returns(video)
                .Verifiable();

            // Act
            var result = _target.Update(1, inputModel);

            // Assert
            result.As<NotFoundObjectResult>().Value.Should().BeEquivalentTo(new { error = "Video not found" });
            result.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "Update_WithInvalidUserId_Should_Return_OK")]
        [Trait("Video update", "Tests behaviors")]
        public void Update_WithInvalidUserId_Should_Return_OK()
        {
            // Arrange
            var inputModel = VideosControllerTestData.GetUpdateVideoInput();
            Video? video = VideosControllerTestData.GetVideos(1).First();

            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.Update(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    )
                )
                .Returns((int id, string? title, string? description, string? url) => {
                    video.Title = !string.IsNullOrEmpty(title) ? title : video.Title;
                    video.Description = !string.IsNullOrEmpty(description) ? description : video.Description;
                    video.Url = !string.IsNullOrEmpty(url) ? url : video.Url;
                    return video;
                })
                .Verifiable();

            // Act
            var result = _target.Update(1, inputModel);

            // Assert
            result.As<OkObjectResult>().Value.Should().BeOfType<VideoModel>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "Delete_Should_Return_OK")]
        [Trait("Video delete", "Tests behaviors")]
        public void Delete_Should_Return_OK()
        {
            // Arrange
            var videoId = new Faker().Random.Int();
            Video? video = new Faker<Video>()
            .Rules((faker, target) =>
            {
                target.Id = videoId;
                target.Title = faker.Commerce.ProductName();
                target.Description = faker.Commerce.ProductDescription();
                target.Url = faker.Internet.Url();
            });

            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.Delete(
                    It.IsAny<int>())
                )
                .Returns(video)
                .Verifiable();

            // Act
            var result = _target.Delete(videoId);

            // Assert
            result.As<OkObjectResult>().Value.Should().BeOfType<VideoModel>();
            result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(new { Id = videoId }, options =>
            {
                options.ExcludingMissingMembers();
                return options;
            });
            _unitOfWorkMock.VerifyAll();
        }

        [Fact(DisplayName = "Delete_WithInvalidUserId_Should_Return_NotFound")]
        [Trait("Video delete", "Tests behaviors")]
        public void Delete_WithInvalidUserId_Should_Return_NotFound()
        {
            // Arrange
            Video? video = null;

            _unitOfWorkMock
                .Setup(mth => mth.VideoRepository.Delete(
                    It.IsAny<int>())
                )
                .Returns(video)
                .Verifiable();

            // Act
            var result = _target.Delete(1);

            // Assert
            result.As<NotFoundObjectResult>().Value.Should().BeEquivalentTo(new { error = "Video not found" });
            result.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
            _unitOfWorkMock.VerifyAll();
        }
    }

    public class VideosControllerTestData
    {
        public static IList<Video> GetVideos(int quantity = 10)
        {
            return new Faker<Video>()
                .Rules((faker, target) =>
                {
                    target.Id = faker.Random.Int();
                    target.Title = faker.Commerce.Department();
                    target.Description = faker.Commerce.ProductName();
                    target.Url = faker.Internet.Url();
                }).Generate(quantity);
        }

        public static CreateVideoInput GetCreateVideoInput()
        {
            return new Faker<CreateVideoInput>()
                .Rules((faker, target) =>
                {
                    target.Title = faker.Vehicle.Model();
                    target.Description = faker.Vehicle.Manufacturer();
                    target.Url = faker.Internet.Url();
                }).Generate();
        }

        public static UpdateVideoInput GetUpdateVideoInput()
        {
            return new Faker<UpdateVideoInput>()
                .Rules((faker, target) =>
                {
                    target.Title = faker.Vehicle.Model();
                    target.Description = faker.Vehicle.Manufacturer();
                    target.Url = faker.Internet.Url();
                }).Generate();
        }
    }
}
