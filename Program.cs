using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NetBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            commands  = new CommandService();

            client.Ready += () =>
            {
                Console.WriteLine("Je suis prêt");
                return Task.CompletedTask;
            };

            client.Log += Log;
            await InstallCommandsAsync();
            await client.LoginAsync(TokenType.Bot,Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
            await client.StartAsync();

            await Task.Delay(-1);
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(),null);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            int argPos = 0;
            if ((message == null) || (!message.HasCharPrefix('$',ref argPos)))
            {
                return;
            }
            var context = new SocketCommandContext(client,message);
            var result = await commands.ExecuteAsync(context,argPos,null);
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);   
            }
        }

        private Task Log(LogMessage args)
        {
            Console.WriteLine(args.ToString());
            return Task.CompletedTask;
        }
    }
}
