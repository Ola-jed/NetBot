using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;

namespace NetBot.Services.Logging;

public class Logger : ILogger
{
    private readonly string _logFileName;

    public Logger(BaseDiscordClient client, CommandService commandService, string? logFileName = null)
    {
        client.Log += Log;
        commandService.Log += Log;
        _logFileName ??= "NetBot.log";
    }

    public async Task Log(LogMessage message)
    {
        Console.ForegroundColor = message.Severity switch
        {
            LogSeverity.Critical or LogSeverity.Error => ConsoleColor.Red,
            LogSeverity.Warning                       => ConsoleColor.Yellow,
            LogSeverity.Verbose or LogSeverity.Debug  => ConsoleColor.DarkGray,
            LogSeverity.Info or _                     => ConsoleColor.White
        };
        var logMsg =
            $"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}";
        Console.WriteLine(logMsg);
        Console.ResetColor();
        await WriteLogsToFile(logMsg);
    }

    public async Task Log(string message, LogLevel level = LogLevel.Information)
    {
        Console.ForegroundColor = level switch
        {
            LogLevel.Error or LogLevel.Critical => ConsoleColor.Red,
            LogLevel.Warning                    => ConsoleColor.Yellow,
            LogLevel.Debug                      => ConsoleColor.DarkGray,
            LogLevel.Information or _           => ConsoleColor.White
        };
        var formattedMessage = $"{DateTime.Now,-19} [{level.LogLevelString(),8}] : {message}";
        Console.WriteLine(formattedMessage);
        Console.ResetColor();
        await WriteLogsToFile(formattedMessage);
    }

    public async Task LogInformation(string message)
    {
        await Log(message);
    }

    public async Task LogDebug(string message)
    {
        await Log(message, LogLevel.Debug);
    }

    public async Task LogWarning(string message)
    {
        await Log(message, LogLevel.Warning);
    }

    public async Task LogError(string message)
    {
        await Log(message, LogLevel.Error);
    }

    public async Task LogCritical(string message)
    {
        await Log(message, LogLevel.Critical);
    }

    private async Task WriteLogsToFile(string logs)
    {
        await using var file = new System.IO.StreamWriter(_logFileName, true);
        await file.WriteLineAsync(logs);
    }
}