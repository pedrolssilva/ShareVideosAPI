using Microsoft.EntityFrameworkCore;
using ShareVideosAPIatabase;
using System.Linq.Expressions;

namespace ShareVideosAPI.Services.Repositories
{
    /// <summary>
    /// Base repository
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContextPostgre _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContextPostgre context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            _context.ChangeTracker.DetectChanges();
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity? GetByKey(params object?[]? keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual TEntity? Insert(TEntity entity)
        {
            TEntity? createdEntity = _dbSet.Add(entity).Entity;
            _context.SaveChanges();
            return createdEntity;
        }
         
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
            _context.SaveChanges();
        }

        public virtual TEntity? Delete(params object?[]? keyValues)
        {
            TEntity? entityToDelete = _dbSet.Find(keyValues);
            if(entityToDelete != null)
            {
                Delete(entityToDelete);
            }
            return entityToDelete;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public virtual void DeleteRange(params TEntity[] entitiesToDelete)
        {
            _dbSet.RemoveRange(entitiesToDelete);
            _context.SaveChanges();
        }

        public virtual List<TEntity> List()
        {
            return _dbSet.ToList();
        }
    }
}
