using Sheduled_Jobs;
using System;
using System.Threading.Tasks; // Required for async Task
using PatientAppUsingJson;

namespace Projects
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("\n========== Main Menu ==========");
                    Console.WriteLine("1 - Send Email Using SMTP (Built-in Method)");
                    Console.WriteLine("2 - Send Email Using MailKit (NuGet Package)");
                    Console.WriteLine("3 - CRUD Operation Using JSON");
                    Console.WriteLine("4 - Task Scheduler");
                    Console.WriteLine("5 - CRUD Operation Using Database");
                    Console.WriteLine("6 - Send Email Using REST API");
                    Console.WriteLine("7 - CRUD Operation Using REST API");
                    Console.WriteLine("8 - Exit");  // <-- New option added here
                    Console.WriteLine("================================");
                    Console.Write("Enter your choice: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            var smtpEmail = new SendEmails();
                            smtpEmail.Send();
                            break;

                        case 2:
                            var mailKitEmail = new SendEmails(); // Example class for MailKit
                            mailKitEmail.Send();
                            break;

                        case 3:
                            var jsonCrud = new CrudOperations();
                            jsonCrud.Menu();
                            break;

                        //case 4:
                        //    var scheduler = new TaskSchedulerJob();
                        //    scheduler.Schedule();
                        //    break;

                        case 5:
                            var dbCrud = new DbPatientCrudoperation();
                            dbCrud.Menu();
                            break;

                        case 6:
                            var restEmail = new RestApiEmailSender();
                            restEmail.SendAsync().Wait();
                            break;

                        case 8:
                            Console.WriteLine("Exiting the program...");
                            exit = true;
                            break;

                        case 7:  // New REST API CRUD case
                            var restApiCrud = new RestAPICrud();
                            restApiCrud.Menu();
                            break;

                        default:
                            Console.WriteLine("Please enter a valid option (1–8).");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }
        }
    }
}
