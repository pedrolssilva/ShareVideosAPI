using Microsoft.EntityFrameworkCore.Storage;
using ShareVideosAPI.Services.Repositories.Videos;
using ShareVideosAPIatabase;

namespace ShareVideosAPI.Services.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContextPostgre _context;
        public IVideoRepository VideoRepository { get; }

        public UnitOfWork(DbContextPostgre context,
            IVideoRepository videoRepository)
        {
            _context = context;
            VideoRepository = videoRepository;
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
