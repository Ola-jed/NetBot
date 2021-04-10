using System;
using System.Threading.Tasks;
using System.Net.Http;
using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;

namespace NetBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Help for the commands")]
        [Alias("Help", "man")]
        public async Task HelpAsync()
        {
            const string info =
            @"
            > --  $help : Get the help <$help>
            > --  $ping : Test the bot <$ping>
            > --  $joke : Read a joke <$joke>
            > --  $avatar : Obtain your profile picture <$avatar>
            > --  $ip : Get the ip where is the ip from <$ip 0.0.0.0>
            > --  $convert : Convert a number from a base to an other one <$convert 'num' from 'base' to 'destination'>
            > --  $react : React to a message with an emoji <$react 'message' 'emoji'>
            > Note : There are some hidden commands";
            await ReplyAsync(info);
        }

        [Command("ping")]
        [Summary("Ping the bot")]
        public async Task PingAsync()
        {
            var msg = await ReplyAsync("> Hello World ");
            await msg.AddReactionAsync(new Emoji("\uD83D\uDEB6"));
        }

        [Command("Avatar")]
        [Summary("Get the user's profile picture")]
        public async Task AvatarAsync(ushort size = 512)
        {
            await ReplyAsync(CDN.GetUserAvatarUrl(Context.User.Id,Context.User.AvatarId,size,ImageFormat.Auto));
        }

        [Command("react")]
        public async Task ReactTask(string arg, string pEmoji = "\uD83D\uDE06")
        {
            var message = await Context.Channel.SendMessageAsync("> " + arg);
            var emoji   = new Emoji(pEmoji);
            await message.AddReactionAsync(emoji);
        }

        [Command("convert")]
        [Summary("Convert a number from a base to another")]
        public async Task ConvertTask(params string[] arg)
        {
            var numberParam = arg[0];
            var initialBase = Convert.ToInt32(arg[2]);
            var destinationBase = Convert.ToInt32(arg[4]);
            try
            {
                var result = await ReplyAsync(Convert.ToString(Convert.ToInt32(numberParam,initialBase),destinationBase));
                await result.AddReactionAsync(new Emoji("\uD83D\uDD22"));
            }
            catch (Exception e)
            {
                var errorToPrint = e.ToString().Split("\n")[0].Split(":")[1];
                var result = await ReplyAsync($"> {errorToPrint}");
                await result.AddReactionAsync(new Emoji("🤕"));
            }
        }

        [Command("joke")]
        public async Task JokeTask()
        {
            var client = new HttpClient();
            string toReturn;
            try
            {
                var response = await client.GetAsync("https://v2.jokeapi.dev/joke/programming");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var res = JObject.Parse(responseBody);
                toReturn = (res["type"]?.ToString() == "single") ? $"{res["joke"]}" : $" {res["setup"]} :  {res["delivery"]}";
            }
            catch(HttpRequestException e)
            {
                toReturn = $"Message :{e.Message}";
            }
            var joke = await ReplyAsync("> "+toReturn);
            await joke.AddReactionAsync(new Emoji("\uD83D\uDE06"));
        }

        [Command("ip")]
        [Summary("Get the country with an ip")]
        public async Task IpTask(string ip)
        {
            var client = new HttpClient();
            string toReturn;
            try
            {
                var response = await client.GetAsync("https://api.ip2country.info/ip?"+ip);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var res  = JObject.Parse(responseBody);
                toReturn = (res["countryName"].ToString() == "") ? "From no Country" :
                        "The ip " + ip + " is from " + res["countryName"].ToString();
            }
            catch(Exception e)
            {
                toReturn = $"Message :{e.Message}";
            }
            await ReplyAsync("> " + toReturn);
        }
    }
}