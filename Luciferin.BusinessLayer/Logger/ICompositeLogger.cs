﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Luciferin.BusinessLayer.Logger
{
    public interface ICompositeLogger<out TCategoryName>
    {
        #region Methods

        Task Log(LogLevel logLevel, string message);

        Task Log(LogLevel logLevel, Exception e, string message);

        Task LogDebug(string message);

        Task LogError(string message);

        Task LogInformation(string message);

        Task LogWarning(string message);

        #endregion
    }
}