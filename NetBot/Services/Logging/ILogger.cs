using System.Threading.Tasks;
using Discord;

namespace NetBot.Services.Logging
{
    public interface ILogger
    {
        Task Log(LogMessage message);
    }
}