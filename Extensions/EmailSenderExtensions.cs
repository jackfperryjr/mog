using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Moogle.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string password, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm Email",
                $"Thanks for signing up!<br/><br/>You've been registered at this email address.<br/><br/>Please confirm your account by clicking this <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>. Your generated password is <strong>" + password + "</strong> and I suggest you change it once you're logged in. By default you won't be able to access any of the controllers for updating characters, monsters, or games but you will have access to the blog. However, if you are interested in contributing just send me an email at jackfperryjr@gmail.com. If you have any suggestions or questions feel free to email me or add a post to the blog!<br/><br/>Thanks again!<br/>Jack");
        }

        public static Task SendRegistrationEmailToAdminAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync("jackfperryjr@gmail.com", "Registration Email",
                $"Hey you,<br/><br/><strong>" + email + "</strong> has registered to the site. I suggest you keep an eye on them.<br/><br/>You from the future/past or something,<br/>Jack");
        }

        public static Task SendUpdateEmailAsync(this IEmailSender emailSender, string subject, string userName, string userEmail, string toOrFrom, string action)
        {
            return emailSender.SendEmailAsync("jackfperryjr@gmail.com", subject,
                $"Hey you,<br/><br/>" + userName + " (" + userEmail + ") has " + action + " a monster " + toOrFrom + " the API. You might want to check it out.");
        }

        public static Task SendBlogPostEmailAsync(this IEmailSender emailSender, string subject, string userName, string userEmail)
        {
            //TODO: Write this email content.
            return emailSender.SendEmailAsync(userEmail, "Copy Of Your Blog Post",
                $"");
        }

        public static Task SendBlogPostToAdminEmailAsync(this IEmailSender emailSender, string content, string userName, string userEmail)
        {
            return emailSender.SendEmailAsync("jackfperryjr@gmail.com", "New Blog Post",
                $"Hey you,<br/><br/>" + userName + " (" + userEmail + ") has posted to the blog.<br/>Here's a copy of the post:<br/><br/>" + content + "<br/><br/>");
        }
    }
}