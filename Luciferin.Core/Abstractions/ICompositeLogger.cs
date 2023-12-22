using Luciferin.Core.Enums;

namespace Luciferin.Core.Abstractions;

public interface ICompositeLogger<out TCategoryName>
{
    Task Log(LogLevel logLevel, string message);

    Task Log(LogLevel logLevel, Exception e, string message);

    Task LogDebug(string message);

    Task LogError(string message);

    Task LogInformation(string message);

    Task LogWarning(string message);
}