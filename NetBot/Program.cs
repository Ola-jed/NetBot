using System.Threading.Tasks;

namespace NetBot
{
    internal static class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var startup = new Startup();
            startup.ConfigureServices();
            await startup.InitClient();
            await startup.Run();
        }
    }
}