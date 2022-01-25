using Luciferin.DataLayer.Storage.Context;
using Luciferin.DataLayer.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luciferin.DataLayer.Storage
{
    public class SettingsDal : BaseDal
    {
        #region Constructors

        /// <inheritdoc />
        public SettingsDal(DbContextOptions<StorageContext> options) : base(options) { }

        #endregion

        #region Methods

        /// <summary>
        /// Get a setting by it's name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <returns></returns>
        public Setting GetSetting(string name)
        {
            return Db.Settings.FirstOrDefault(s => string.Equals(s.Name, name));
        }

        /// <summary>
        /// Get all the settings.
        /// </summary>
        /// <returns></returns>
        public ICollection<Setting> GetSettings()
        {
            return Db.Settings.ToList();
        }

        #endregion
    }
}