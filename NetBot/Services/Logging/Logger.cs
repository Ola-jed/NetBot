using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;

namespace NetBot.Services.Logging
{
    public class Logger : ILogger
    {
        public Logger(BaseDiscordClient client, CommandService commandService)
        {
            client.Log += Log;
            commandService.Log += Log;
        }

        public Task Log(LogMessage message)
        {
            Console.ForegroundColor = message.Severity switch
            {
                LogSeverity.Critical or LogSeverity.Error => ConsoleColor.Red,
                LogSeverity.Warning                       => ConsoleColor.Yellow,
                LogSeverity.Verbose or LogSeverity.Debug  => ConsoleColor.DarkGray,
                LogSeverity.Info or _                     => ConsoleColor.White
            };
            Console.WriteLine(
                $"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}