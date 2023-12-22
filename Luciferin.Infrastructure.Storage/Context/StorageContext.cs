using Luciferin.Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.Infrastructure.Storage.Context
{
    public class StorageContext : DbContext
    {
        #region Properties

        public DbSet<ImportStatistic> ImportStatistics { get; set; }

        public DbSet<Setting> Settings { get; set; }

        #endregion

        #region Constructors

        public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }

        #endregion
    }
}