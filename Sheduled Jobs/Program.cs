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
                var a = Convert.ToInt32(Console.ReadLine());
                switch (a)
                {
                    case 1:
                        var test = new SendEmails();
                        test.Send();
                        break;
                    case 2:
                        CrudOperations operations = new CrudOperations();
                        operations.Menu();
                        break;
                    case 3:
                    //TaskScheduler task = new TaskScheduler();
                    //task.Shedule();
                    //break;
                    case 4:
                        DbPatientCrudoperation DB = new DbPatientCrudoperation();
                        DB.Menu();
                        break;
                    
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
