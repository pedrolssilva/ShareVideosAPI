using Microsoft.EntityFrameworkCore.Storage;
using ShareVideosAPI.Services.Repositories.Categories;
using ShareVideosAPI.Services.Repositories.Videos;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.Services.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContextPostgre _context;
        public IVideoRepository VideoRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(DbContextPostgre context,
            IVideoRepository videoRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            VideoRepository = videoRepository;
            CategoryRepository = categoryRepository;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
