using Microsoft.EntityFrameworkCore.Storage;
using ShareVideosAPI.Services.Repositories.Videos;

namespace ShareVideosAPI.Services.Database
{
    public interface IUnitOfWork
    {
        IVideoRepository VideoRepository { get; }
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
