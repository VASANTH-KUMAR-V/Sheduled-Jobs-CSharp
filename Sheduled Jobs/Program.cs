using Sheduled_Jobs;
using System;

namespace AutomatedEmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            SendEmails toemail = new SendEmails();
            toemail.send();
        }
    }
}
