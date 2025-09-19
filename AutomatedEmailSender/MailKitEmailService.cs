    using System;
    using System.Threading.Tasks;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;

    namespace AutomatedEmailSender
    {
        public class MailKitEmailService : EmailSender
        {
            private string GmailAppPassword;

            public MailKitEmailService(string fromAddress, string toAddress, string subject, string content, string gmailAppPassword)
                : base(fromAddress, toAddress, subject, content)
            {
                GmailAppPassword = gmailAppPassword;
            }

            public override void SendEmail()
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Sender Name",FromAddress));
                email.To.Add(new MailboxAddress("Recipient Name", ToAddress));
                email.Subject = Subject;
                email.Body = new TextPart("plain")
                {
                    Text = Content
                };
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true; 

                    Console.WriteLine("Connecting to SMTP server...");
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    Console.WriteLine("Connected.");

                     smtp.Authenticate(FromAddress, GmailAppPassword);
                     smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
            
            }
        }
    }
