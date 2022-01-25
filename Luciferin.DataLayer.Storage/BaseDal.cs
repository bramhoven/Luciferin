using Luciferin.DataLayer.Storage.Context;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.DataLayer.Storage
{
    public abstract class BaseDal : IDisposable
    {
        #region Fields

        protected readonly StorageContext Db;

        #endregion

        #region Constructors

        protected BaseDal(DbContextOptions<StorageContext> options) : this(new StorageContext(options)) { }

        protected BaseDal(StorageContext db)
        {
            Db = db;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            Db?.Dispose();
        }

        public void Save()
        {
            Db.SaveChanges();
        }

        #endregion
    }
}