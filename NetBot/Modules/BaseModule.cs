using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NetBot.Models;
using NetBot.Services.Logging;
using NetBot.Services.PigLatin;

namespace NetBot.Modules;

[Name("Base")]
[Summary("Some basic commands")]
public class BaseModule: ModuleBase<SocketCommandContext>
{
    private readonly HttpClient _client;
    private readonly ILogger _logger;

    public BaseModule(HttpClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

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
        string toReturn;
        try
        {
            var response = await _client.GetAsync("https://v2.jokeapi.dev/joke/programming");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var jokeResponse = await JsonSerializer.DeserializeAsync<JokeApiResponse>(responseStream);
            toReturn = (jokeResponse?.Type == "single"
                ? jokeResponse.Joke
                : $"{jokeResponse?.Setup} :\n{jokeResponse?.Delivery}") ?? string.Empty;
        }
        catch(HttpRequestException e)
        {
            await _logger.LogWarning($"{GetType().Name}.{nameof(GetJoke)} : {e.Message}");
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