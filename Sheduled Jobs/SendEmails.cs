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
            fromAddress = "vasanthvc0211@gmail.com";

            // Get dynamic user input for 'toAddress', 'subject', and 'content'
            Console.WriteLine("Enter the recipient's email address:");
            var toAddress = Console.ReadLine();

            Console.WriteLine("Enter CC email address (optional):");
            var ccaddress = Console.ReadLine();

            Console.WriteLine("Enter BCC email address (optional):");
            var bccaddress = Console.ReadLine();

            Console.WriteLine("Enter the email subject:");
            var subject = Console.ReadLine();

            Console.WriteLine("Enter the email content (HTML allowed):");
            var content = Console.ReadLine();

            Console.WriteLine("Enter the scheduled time to send the email (e.g., 2025-09-22 17:30):");
            var inputTime = Console.ReadLine();

            if (!DateTime.TryParse(inputTime, out DateTime targetTime))
            {
                Console.WriteLine("Invalid date/time format.");
                return;
            }

            TimeSpan delay = targetTime - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                Console.WriteLine($"Waiting until {targetTime} to send the email...");
                Task.Delay(delay).GetAwaiter().GetResult(); // Blocking wait
            }
            else
            {
                Console.WriteLine("The scheduled time is in the past. Sending email immediately.");
            }


            //Create the BuiltinEmailService object with user input
            var emailSender = new BuiltinEmailService(fromAddress, toAddress, ccaddress, bccaddress, subject, content, gmailAppPassword);
            emailSender.SendEmail();

            //Create the MailAutomatedEmail object with user input
            //var email = new MailKitEmailService(fromAddress, toAddress, subject, content, gmailAppPassword);
            //email.SendEmail();

        }

    }
}
