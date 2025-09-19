using Sheduled_Jobs;
using System;
using System.Threading.Tasks; // Required for async Task

namespace AutomatedEmailSender
{
    class Program
    {
        public static void Main(string[] args)
        {
            var test = new SendEmails();
            test.Send();
        }
    }
}
