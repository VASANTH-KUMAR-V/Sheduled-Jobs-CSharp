using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Sheduled_Jobs
{
    class Test
    {
        private string gmailAppPassword = "ebip gwqo kpzr hlym";
        private string fromAddress = "vasanthvc0211@gmail.com";
        private string toAddress = "vasanthvc02@gmail.com";
        private string subject = "MailKit Test";
        private string content = "This is a test email sent using MailKit in C#.";

        public async Task SendEmailAsync()
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sender Name", fromAddress));
            email.To.Add(new MailboxAddress("Recipient Name", toAddress));
            email.Subject = subject;

            email.Body = new TextPart("plain")
            {
                Text = content
            };

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true; // For debugging only

                    Console.WriteLine("Connecting to SMTP server...");
                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    Console.WriteLine("Connected.");

                    await smtp.AuthenticateAsync(fromAddress, gmailAppPassword);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);

                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }
    }
}
