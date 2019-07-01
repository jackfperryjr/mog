using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Moogle.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm Email",
                $"Thanks for helping out!<br/><br/>I've registered you at this email address and your default password is: <strong>p@ssW0rd</strong><br/><br/>Please confirm your account by clicking this <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>. I suggest you change your password once you're logged in. You will currently have access to modifying data for monsters only. If you have any suggestions or questions feel free to email me.<br/><br/>Thanks again!<br/>Jack");
        }

        public static Task SendRegistrationEmailToAdminAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync("jackfperryjr@gmail.com", "Registration Email",
                $"Hey you,<br/><br/>You've registered <strong>" + email + "</strong> to help you out. I suggest you keep an eye on them.<br/><br/>You from the future/past or something,<br/>Jack");
        }

        public static Task SendUpdateEmailAsync(this IEmailSender emailSender, string subject, string userName, string userEmail, string toOrFrom, string action)
        {
            return emailSender.SendEmailAsync("jackfperryjr@gmail.com", subject,
                $"Hey you,<br/><br/>" + userName + " (" + userEmail + ") has " + action + " a monster " + toOrFrom + " the API. You might want to check it out.");
        }
    }
}