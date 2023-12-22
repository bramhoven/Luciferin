using System;
using Luciferin.Infrastructure.Storage.Context;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.Infrastructure.Storage
{
    public abstract class BaseDal : IDisposable
    {
        #region Fields

        protected readonly StorageContext Db;

        #endregion

        #region Constructors

        protected BaseDal(DbContextOptions<StorageContext> options) : this(new StorageContext(options)) { }

        private BaseDal(StorageContext db)
        {
            Db = db;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            Db.Dispose();
        }

        public void Save()
        {
            Db.SaveChanges();
        }

        #endregion
    }
}