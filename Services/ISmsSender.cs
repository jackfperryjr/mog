using System.Threading.Tasks;

namespace Mog.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}