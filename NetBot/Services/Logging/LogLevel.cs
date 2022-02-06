namespace NetBot.Services.Logging;

public enum LogLevel
{
    Information,
    Debug,
    Warning,
    Error,
    Critical
}

public static class LogLevelExtensions
{
    public static string LogLevelString(this LogLevel l)
    {
        return l switch
        {
            LogLevel.Information => "Info",
            LogLevel.Debug       => "Debug",
            LogLevel.Warning     => "Warn",
            LogLevel.Error       => "Error",
            LogLevel.Critical    => "Critical",
            _                    => "Unknown"
        };
    }
}