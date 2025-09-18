using System;

namespace AutomatedEmailSender
{
    public abstract class EmailService
    {
        public string FromAddress;
        public string ToAddress;
        public string Subject;
        public string Content;


        public EmailService(string fromaddress, string toaddress, string subject, string content)
        {
            FromAddress = fromaddress;
            ToAddress = toaddress;
            Subject = subject;
            Content = content;
        }

        public abstract void SendEmail();
    }
}
