using AutoMapper;
using ShareVideosAPI.Models.Video;
using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPI.Services.Mapper.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoModel>().ReverseMap();
        }
    }
}
