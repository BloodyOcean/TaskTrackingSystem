using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Creates new message object and sends this message using SMTP.
        /// Should be injected. Only works for testing and with approved emails
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Administration", "test@tmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mailgun.org", 587, false);
                await client.AuthenticateAsync("postmaster@sandbox8f8ac4738be34f69bd519dd7fcf3067a.mailgun.org", "d3a172aa5747f4160b4e9ae53d68a0e5-cac494aa-80bb9ef8");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
