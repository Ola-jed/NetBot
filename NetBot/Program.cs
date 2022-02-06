using System;
using System.Threading.Tasks;
using NetBot.Services.Logging;

namespace NetBot;

public static class Program
{
    private static Startup? _startup;

    public static async Task Main()
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        _startup = new Startup();
        _startup.ConfigureServices();
        await _startup.InitClient();
        await _startup.Run();
    }

    private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        _startup?.GetService<ILogger>()?.LogCritical($"Unhandled exception: {e.ExceptionObject}")
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();
    }
}