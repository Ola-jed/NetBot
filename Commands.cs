using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace NetBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            const string info =
            @"
--  $help : Get the help
--  $ping : Test the bot
--  $joke : Read a joke
--  $avatar : Obtain your profile picture
--  $ip : Get the ip where is the ip from <$ip 0.0.0.0>
--  $convert : Convert a number from a base to an other one <$convert 'num' from 'base' to 'destination'>
--  $react : Réagir à un message avec un emoji <$react 'message' 'emoji'>
Note : There are some hidden commands";
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
        public async Task ReactTask(string arg, string pEmoji = "🙂")
        {
            var message = await Context.Channel.SendMessageAsync(arg);
            var emoji   = new Emoji(pEmoji);

            await message.AddReactionAsync(emoji);
        }

        [Command("convert")]
        public async Task ConvertTask(string num1,string from,string baseNum,string to,string destination)
        {
            var number   = num1;
            var fromBase = Convert.ToInt32(baseNum);
            int toBase   = Convert.ToInt32(destination);
            await ReplyAsync(Convert.ToString(Convert.ToInt32(number, fromBase), toBase));
        }

        [Command("joke")]
        public async Task JokeTask()
        {
            HttpClient client = new HttpClient();
            string toReturn;
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://v2.jokeapi.dev/joke/programming");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var res = JObject.Parse(responseBody);
                toReturn = (res["type"].ToString() == "single") ? $"{res["joke"]}" : $" {res["setup"]} :  {res["delivery"]}";
            }
            catch(HttpRequestException e)
            {
                toReturn = $"Message :{e.Message}";
            }
            await ReplyAsync(toReturn);
        }

        [Command("ip")]
        public async Task IpTask(string ip)
        {
            HttpClient client = new HttpClient();
            string toReturn;
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.ip2country.info/ip?"+ip);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var res  = JObject.Parse(responseBody);
                toReturn = (res["countryName"].ToString() == "") ? "From no Country" :
                        "The ip " + ip + " is from " + res["countryName"].ToString();
            }
            catch(HttpRequestException e)
            {
                toReturn = $"Message :{e.Message}";
            }
            await ReplyAsync(toReturn);
        }

        [Command("getthis")]
        public async Task GetthisTask()
        {
            await ReplyAsync("Come on my website : https://gettthiss.000webhostapp.com");
        }
    }
}