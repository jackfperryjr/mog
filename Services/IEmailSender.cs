using System.Threading.Tasks;

namespace Moogle.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}