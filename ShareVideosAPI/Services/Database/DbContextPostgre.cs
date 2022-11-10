using Microsoft.EntityFrameworkCore;
using Npgsql;
using ShareVideosAPI.Services.Database;
using ShareVideosAPI.Services.Database.Configuraion;
using ShareVideosAPI.Services.Entities;

namespace ShareVideosAPIatabase
{
    /// <summary>
    /// Custom DbContext for Postgre database
    /// </summary>
    public class DbContextPostgre : DbContext
    {
        /// <summary>
        /// Videos table
        /// </summary>
        public DbSet<Video> Videos => Set<Video>();
        public DbSet<Category> Categories => Set<Category>();

        /// <summary>
        /// Create the database context with PostgreDB
        /// </summary>
        /// <param name="options"> Context options </param>
        public DbContextPostgre(DbContextOptions options) : base(options)
        {
            InitTypeMappings();
        }

        public static void InitTypeMappings()
        {
            //NpgsqlConnection.GlobalTypeMapper.MapEnum<EnumType>();
        }

        /// <summary>
        /// Configure the database model (relations, keys, indexes)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasPostgresEnum<EnumType>();

            modelBuilder.ApplyConfiguration(new VideoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                if (Database.CurrentTransaction != null)
                {
                    Database.RollbackTransaction();
                }
                throw DatabaseException.Generate(ex.Message, ex.InnerException);
            }
        }
    }
}
