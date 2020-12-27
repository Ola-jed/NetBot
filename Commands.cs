using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NetBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            const string info =
            @"
-- $help : Obtenir l'aide
--  $ping : Pour tester le bot
--  $avatar : Obtenir votre photo de profil
--  $react : Réagir à un message avec un emoji";
            await ReplyAsync(info);
        }

        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Hello World ");
        }

        [Command("Avatar")]
        public async Task AvatarAsync(ushort size = 512)
        {
            await ReplyAsync(CDN.GetUserAvatarUrl(Context.User.Id,Context.User.AvatarId,size,ImageFormat.Auto));
        }

        [Command("react")]
        public async Task ReactTask(string arg, string pEmoji)
        {
            var message = await Context.Channel.SendMessageAsync(arg);
            var emoji   = new Emoji(pEmoji);

            await message.AddReactionAsync(emoji);
        }

        [Command("getthis")]
        public async Task GetthisTask()
        {
            await ReplyAsync("Viens sur mon site : https://gettthiss.000webhostapp.com");
        }
    }
}