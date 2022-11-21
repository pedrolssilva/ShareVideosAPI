using Microsoft.EntityFrameworkCore;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Database.Seeds;
using ShareVideosAPI.Services.Repositories.Categories;
using ShareVideosAPI.Services.Repositories.Videos;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.DependencyInjection
{
    public static class DependencyInjectionDB
    {
        public static IServiceCollection AddInfrastructureDB(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Server");

            services.AddDbContext<DbContextPostgre>(options => options.UseNpgsql(connectionString));
            services.AddTransient<IVideoRepository, VideoRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISeedData, SeedData>();

            return services;
        }
    }
}
