using Luciferin.DataLayer.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.DataLayer.Storage.Context
{
    public class StorageContext : DbContext
    {
        #region Properties

        public DbSet<Setting> Settings { get; set; }

        #endregion

        #region Constructors

        public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }

        #endregion
    }
}