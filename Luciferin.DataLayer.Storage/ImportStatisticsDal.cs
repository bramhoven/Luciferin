using System;
using System.Linq;
using Luciferin.DataLayer.Storage.Context;
using Luciferin.DataLayer.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.DataLayer.Storage
{
    public class ImportStatisticsDal : BaseDal
    {
        #region Constructors

        /// <inheritdoc />
        public ImportStatisticsDal(DbContextOptions<StorageContext> options) : base(options) { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the date time of the last ran import.
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastImportDateTime()
        {
            return Db.ImportStatistics.OrderByDescending(i => i.ImportDate).FirstOrDefault()?.ImportDate ?? DateTime.MinValue;
        }

        /// <summary>
        /// Inserts a new import statistic.
        /// </summary>
        /// <param name="importStatistic">The import statistic that has to be inserted.</param>
        public void InsertImportStatistics(ImportStatistic importStatistic)
        {
            Db.ImportStatistics.Add(importStatistic);
            Db.SaveChanges();
        }

        #endregion
    }
}