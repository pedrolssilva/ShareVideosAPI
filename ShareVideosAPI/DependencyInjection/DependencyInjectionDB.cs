using Microsoft.EntityFrameworkCore;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Repositories.Videos;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.DependencyInjection
{
    public static class DependencyInjectionDB
    {
        public static IServiceCollection AddInfrastructureDB(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Server");

            services.AddDbContext<DbContextPostgre>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
