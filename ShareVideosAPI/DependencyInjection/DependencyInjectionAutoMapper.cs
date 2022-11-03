using ShareVideosAPI.Services.Mapper.Profiles;

namespace ShareVideosAPI.DependencyInjection
{
    public static class DependencyInjectionAutoMapper
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<VideoProfile>();
            });
            return services;
        }
    }
}
