using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NetBot.Services.PigLatin;

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

        [Command("Joke")]
        [Summary("Read a random joke")]
        public async Task GetJoke()
        {
            var client = new HttpClient();
            string toReturn;
            try
            {
                var response = await client.GetAsync("https://v2.jokeapi.dev/joke/programming");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var res = JsonDocument.Parse(responseBody).RootElement;
                toReturn = (res.GetProperty("type").GetString() == "single")
                    ? $"{res.GetProperty("joke").GetString()}"
                    : $" {res.GetProperty("setup").GetString()} : {res.GetProperty("delivery").GetString()}";
            }
            catch(HttpRequestException)
            {
                toReturn = "Something went wrong";
            }

            var joke = await ReplyAsync("> "+toReturn);
            await joke.AddReactionAsync(new Emoji("\uD83D\uDE06"));
        }

        [Command("Pigify")]
        [Summary("Translate a sentence to pig latin")]
        public async Task Pigify(params string[] args)
        {
            var builder = new EmbedBuilder
            {
                Color = Color.LightOrange
            };
            var pigifiedData = args.Select(arg => arg.Pigify()).ToArray();
            builder.AddField("Pig says", string.Join(" ",pigifiedData));
            await ReplyAsync("", false, builder.Build());
        }
    }
}