using System.Linq.Expressions;

namespace ShareVideosAPI.Services.Repositories
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");
        TEntity? GetByKey(params object?[]? keyValues);
        TEntity? Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        TEntity? Delete(params object?[]? keyValues);
        void Delete(TEntity entityToDelete);
        void DeleteRange(params TEntity[] entitiesToDelete);
        List<TEntity> List();
    }
}
