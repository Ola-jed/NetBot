using System.Threading.Tasks;
using Discord.Commands;

namespace NetBot.Modules
{
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