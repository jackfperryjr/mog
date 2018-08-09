using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Moogle.Services;

namespace Moogle.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Thanks for helping out!<br/><br/>I've registered you at this email address and your default password is: <strong>p@ssW0rd</strong><br/><br/>Please confirm your account by clicking this <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>. I suggest you change your password once you're logged in. You will have access to modifying data for games and monsters, especially monsters! If you have any suggestions or questions feel free to email me at this address.<br/><br/>Thanks again!<br/>Jack");
        }

        public static Task SendUpdateEmailAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Update",
                $"Something was updated.");
        }
    }
}
