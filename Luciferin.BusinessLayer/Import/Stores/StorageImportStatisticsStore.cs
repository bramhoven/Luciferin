using System;
using AutoMapper;
using Luciferin.BusinessLayer.Import.Models;
using Luciferin.Infrastructure.Storage;
using Luciferin.Infrastructure.Storage.Entities;

namespace Luciferin.BusinessLayer.Import.Stores
{
    public class StorageImportStatisticsStore : IImportStatisticsStore
    {
        #region Fields

        private readonly ImportStatisticsDal _importStatisticsDal;

        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public StorageImportStatisticsStore(ImportStatisticsDal importStatisticsDal, IMapper mapper)
        {
            _importStatisticsDal = importStatisticsDal;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public DateTime GetLastImportDateTime()
        {
            return _importStatisticsDal.GetLastImportDateTime();
        }

        /// <inheritdoc />
        public void InsertImportStatistic(Statistic statistic)
        {
            var entity = _mapper.Map<ImportStatistic>(statistic);
            _importStatisticsDal.InsertImportStatistics(entity);
        }

        #endregion
    }
}