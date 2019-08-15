using System.Threading.Tasks;

namespace Moogle.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}