using AutoMapper;
using Luciferin.BusinessLayer.Import.Models;
using Luciferin.DataLayer.Storage.Entities;

namespace Luciferin.BusinessLayer.Mapping
{
    public class StatisticsProfile : Profile
    {
        #region Constructors

        public StatisticsProfile()
        {
            CreateMap<Statistic, ImportStatistic>();
        }

        #endregion
    }
}