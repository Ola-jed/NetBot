using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using NetBot.TypeReaders;

namespace NetBot.Services;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(IServiceProvider services)
    {
        _commands = services.GetRequiredService<CommandService>();
        _client = services.GetRequiredService<DiscordSocketClient>();
        _services = services;
    }

    public async Task InitializeAsync()
    {
        _commands.AddTypeReader(typeof(Tuple<string, int, int>), new ConversionTypeReader());
        await _commands.AddModulesAsync(
            Assembly.GetEntryAssembly(),
            _services);
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage msg)
    {
        if (msg is not SocketUserMessage message)
        {
            return;
        }

        var argPos = 0;
        if (!(message.HasCharPrefix('$', ref argPos)
              || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            || message.Author.IsBot)
        {
            return;
        }

        var context = new SocketCommandContext(_client, message);
        await _commands.ExecuteAsync(
            context,
            argPos,
            _services);
    }
}