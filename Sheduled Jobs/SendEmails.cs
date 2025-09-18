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
        public void  send() {

            gmailAppPassword = "ebip gwqo kpzr hlym";

            fromAddress = "vasanthvc0211@gmail.com";

            // Get dynamic user input for 'toAddress', 'subject', and 'content'
            Console.WriteLine("Enter the recipient's email address:");
            string toAddress = Console.ReadLine();

            Console.WriteLine("Enter the email subject:");
            string subject = Console.ReadLine();

            Console.WriteLine("Enter the email content (HTML allowed):");
            string content = Console.ReadLine();

            // Create the SendAutomatedEmail object with user input
            SendAutomatedEmail emailSender = new SendAutomatedEmail(fromAddress, toAddress, subject, content, gmailAppPassword);

            // Send the email
            emailSender.SendEmail();
        }
    }
}
