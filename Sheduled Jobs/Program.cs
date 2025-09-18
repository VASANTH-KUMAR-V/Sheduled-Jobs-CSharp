using System;
using System.Net;

namespace Sheduled_Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSender emailSender = new EmailSender();

            // Call the SendEmail method to send the email
            emailSender.SendEmail();

            // Wait for user input before closing the console (so you can see the output)
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
