using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NetBot.Services;
using NetBot.Services.Logging;

namespace NetBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;

        public static void Main() => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
            });
            await _client.SetGameAsync("Being refactored");
            _commandService = new CommandService();
            var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            var logger = new Logger(_client,_commandService);
            var init = new Startup(_commandService, _client, logger);
            var commandHandler = new CommandHandler(init.ConfigureServices());
            await commandHandler.InitializeAsync();
            await Task.Delay(-1);
        }
    }
}