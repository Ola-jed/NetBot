using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using NetBot.Services;
using NetBot.Services.Logging;
using NetBot.Services.Math;

namespace NetBot;

public class Startup
{
    private readonly CommandService _commands;
    private readonly DiscordSocketClient _client;
    private readonly ILogger _logger;
    private readonly IMathService _mathService;
    private IServiceProvider _services = null!;

    public Startup()
    {
        _commands = new CommandService();
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Verbose
        });
        _logger = new Logger(_client, _commands);
        _mathService = new MathService();
    }

    public void ConfigureServices()
    {
        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_logger)
            .AddHttpClient()
            .AddSingleton(_mathService)
            .AddSingleton<CommandHandler>()
            .BuildServiceProvider();
    }

    public T? GetService<T>()
    {
        return _services.GetService<T>();
    }
    
    public async Task InitClient()
    {
        await _client.SetGameAsync("Being refactored");
        var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
    }

    public async Task Run()
    {
        var commandHandler = new CommandHandler(_services);
        await commandHandler.InitializeAsync();
        await Task.Delay(-1);
    }
}