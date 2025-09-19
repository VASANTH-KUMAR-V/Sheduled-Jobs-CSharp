using System;
using System.Threading.Tasks;

namespace AutomatedEmailSender
{
    public abstract class EmailSender
    {
        public string FromAddress;
        public string ToAddress;
        public string Subject;
        public string Content;


        public EmailSender(string fromaddress, string toaddress, string subject, string content)
        {
            FromAddress = fromaddress;
            ToAddress = toaddress;
            Subject = subject;
            Content = content;
        }

        public abstract void SendEmail();
    }
}
