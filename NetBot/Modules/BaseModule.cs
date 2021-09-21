using System.Threading.Tasks;
using Discord.Commands;

namespace NetBot.Modules
{
    [Name("Base")]
    [Summary("Some basic commands")]
    public class BaseModule: ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Ping the bot")]
        public async Task Ping()
        {
            await ReplyAsync("pong !");
        }
    }
}