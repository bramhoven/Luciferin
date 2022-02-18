using System;
using Luciferin.BusinessLayer.Import.Models;

namespace Luciferin.BusinessLayer.Import.Stores
{
    public interface IImportStatisticsStore
    {
        #region Methods

        /// <summary>
        /// Gets the date time of the last ran import.
        /// </summary>
        /// <returns></returns>
        DateTime GetLastImportDateTime();

        /// <summary>
        /// Inserts a new import statistic.
        /// </summary>
        /// <param name="statistic">The import statistic to insert.</param>
        void InsertImportStatistic(Statistic statistic);

        #endregion
    }
}