using System;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using NetBot.Services.Logging;

namespace NetBot.TypeReaders;

public class ConversionTypeReader : TypeReader
{
    /// <summary>
    /// $convert number from base(int) to base(int)
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <param name="services"></param>
    /// <returns></returns>
    public override Task<TypeReaderResult> ReadAsync(ICommandContext context,
        string input,
        IServiceProvider services)
    {
        try
        {
            var content = input.Split("-");
            var number = content[0];
            var fromBase = Convert.ToInt32(content[1]);
            var destinationBase = Convert.ToInt32(content[2]);
            var result = new Tuple<string, int, int>(number, fromBase, destinationBase);
            return Task.FromResult(TypeReaderResult.FromSuccess(result));
        }
        catch (Exception e)
        {
            services.GetService<ILogger>()?.LogError($"{GetType().Name} failed to parse input: {input}")
                .ContinueWith(_ => { });
            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed"));
        }
    }
}