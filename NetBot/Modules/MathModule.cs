using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace NetBot.Modules
{
    [Name("Math")]
    [Summary("Math module for some math things")]
    public class MathModule : ModuleBase<SocketCommandContext>
    {
        [Command("sum")]
        [Summary("Sum numbers of a sequence")]
        public async Task Sum(params int[] args)
        {
            var builder = new EmbedBuilder()
            {
                Color = Color.Teal
            };
            builder.AddField("Result", args.Sum());
            await ReplyAsync("", false, builder.Build());
        }

        [Command("mul")]
        [Summary("Multiply the numbers of a sequence")]
        public async Task Mul(params int[] args)
        {
            var builder = new EmbedBuilder()
            {
                Color = Color.DarkPurple
            };
            builder.AddField("Result", args.Aggregate((x, y) => x * y));
            await ReplyAsync("", false, builder.Build());
        }

        [Command("eval")]
        [Summary("Evaluate a mathematical expression")]
        public async Task Eval(params string[] expression)
        {
            var builder = new EmbedBuilder()
            {
                Color = Color.DarkMagenta
            };
            var result = new DataTable().Compute(string.Concat(expression), null);
            builder.AddField("Result", result);
            await ReplyAsync("", false, builder.Build());
        }
    }
}