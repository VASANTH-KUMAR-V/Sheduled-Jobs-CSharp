using System;

namespace AutomatedEmailSender
{
    public abstract class EmailService
    {
        public string FromAddress;
        public string ToAddress;
        public string Subject;
        public string Content;
        public string AppPassword;

        public abstract void Email(string fromaddress, string toaddress, string subject, string content);
    }
}
