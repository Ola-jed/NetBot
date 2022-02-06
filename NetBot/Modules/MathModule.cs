using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NetBot.Services.Math;

namespace NetBot.Modules;

[Name("Math")]
[Summary("Math module for some math things")]
public class MathModule : ModuleBase<SocketCommandContext>
{
    private readonly IMathService _mathService;

    public MathModule(IMathService mathService)
    {
        _mathService = mathService;
    }

    [Command("sum")]
    [Summary("Sum numbers of a sequence")]
    public async Task Sum(params ulong[] args)
    {
        var builder = new EmbedBuilder
        {
            Color = Color.Teal
        };
        builder.AddField("Result", _mathService.Sum(args));
        await ReplyAsync("", false, builder.Build());
    }

    [Command("mul")]
    [Summary("Multiply the numbers of a sequence")]
    public async Task Mul(params ulong[] args)
    {
        var builder = new EmbedBuilder
        {
            Color = Color.DarkPurple
        };
        builder.AddField("Result", _mathService.Multiply(args));
        await ReplyAsync("", false, builder.Build());
    }

    [Command("eval")]
    [Summary("Evaluate a mathematical expression")]
    public async Task Eval(params string[] expression)
    {
        var builder = new EmbedBuilder
        {
            Color = Color.DarkMagenta
        };
        builder.AddField("Result", _mathService.Eval(expression));
        await ReplyAsync("", false, builder.Build());
    }

    [Command("convert")]
    [Summary("Convert a number from one base to another, syntax is $convert number-from-to")]
    public async Task Convert(Tuple<string,int,int> conversionData)
    {
        var builder = new EmbedBuilder
        {
            Color = Color.Red
        };
        var (baseNumber, fromBase, toBase) = conversionData;
        builder.AddField("Conversion result",_mathService.Convert(baseNumber, fromBase, toBase));
        await ReplyAsync("", false, builder.Build());
    }
}