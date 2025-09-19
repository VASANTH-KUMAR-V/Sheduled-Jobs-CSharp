using AutomatedEmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheduled_Jobs
{
    class SendEmails
    {
        public string gmailAppPassword = null;
        public string fromAddress = null;
        public void Send() {

            gmailAppPassword = "ebip gwqo kpzr hlym";

            // Get dynamic user input for 'toAddress', 'subject', and 'content'
            Console.WriteLine("Enter the recipient's email address:");
            var toAddress = Console.ReadLine();

            Console.WriteLine("Enter the email subject:");
            var subject = Console.ReadLine();

            Console.WriteLine("Enter the email content (HTML allowed):");
            var content = Console.ReadLine();

            //Create the BuiltinEmailService object with user input
            //var emailSender = new BuiltinEmailService(fromAddress, toAddress, subject, content, gmailAppPassword);
            //emailSender.SendEmail();

            //Create the MailAutomatedEmail object with user input
            //var email = new MailKitEmailService(fromAddress, toAddress, subject, content, gmailAppPassword);
            //email.SendEmail();

        }

    }
}
