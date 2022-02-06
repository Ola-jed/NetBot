using System.Threading.Tasks;
using Discord;

namespace NetBot.Services.Logging;

public interface ILogger
{
    Task Log(LogMessage message);
    Task Log(string message,LogLevel level = LogLevel.Information);
    Task LogInformation(string message);
    Task LogDebug(string message);
    Task LogWarning(string message);
    Task LogError(string message);
    Task LogCritical(string message);
    
}