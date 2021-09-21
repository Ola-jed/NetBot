using System;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using NetBot.Services;
using NetBot.Services.Logging;

namespace NetBot
{
    public class Initialize
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly ILogger _logger;

        public Initialize(CommandService commands, DiscordSocketClient client,ILogger logger)
        {
            _commands = commands;
            _client = client;
            _logger = logger;
        }

        public IServiceProvider BuildServiceProvider() => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_logger)
            .AddSingleton<CommandHandler>()
            .BuildServiceProvider();
    }
}