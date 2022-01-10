using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Luciferin.BusinessLayer.Logger
{
    public interface ICompositeLogger<out TCategoryName>
    {
        #region Methods

        Task Log(LogLevel logLevel, string message);

        Task Log(LogLevel logLevel, Exception e, string message);

        Task LogInformation(string message);

        #endregion
    }
}