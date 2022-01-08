using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FireflyImporter.BusinessLayer.Logger
{
    public interface ICompositeLogger
    {
        #region Methods

        Task Log(LogLevel logLevel, string message);

        Task LogInformation(string message);

        void SetLogger(ILogger logger);

        #endregion
    }
}