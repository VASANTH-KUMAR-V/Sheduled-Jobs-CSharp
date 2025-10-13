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
                Console.WriteLine("Enter the Number to perform the Exercise");
                Console.WriteLine("1 - Send Email Using SMTP Buildin Method");
                Console.WriteLine("2 - Send Email Using Mailkit Nuget Packages Method");
                Console.WriteLine("3 - CRUD Operation using JSON");
                Console.WriteLine("4 - Task Sheduler");
                Console.WriteLine("5 - CRUD Operation using Database");
                Console.WriteLine("5 - Exit");
                var a = Convert.ToInt32(Console.ReadLine());
                switch (a)
                {
                    case 1:
                        var test = new SendEmails();
                        test.Send();
                        break;
                    case 3:
                        CrudOperations operations = new CrudOperations();
                        operations.Menu();
                        break;
                    case 4:
                        //TaskScheduler.Shedule();
                        //break;
                    case 5:
                        DbPatientCrudoperation DB = new DbPatientCrudoperation();
                        DB.Menu();
                        break;
                    case 6:
                        Console.WriteLine("Exiting.....");
                        return;
                    default:
                        Console.Write("Enter the correct number: ");
                        break;

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }

        }
    }
}
