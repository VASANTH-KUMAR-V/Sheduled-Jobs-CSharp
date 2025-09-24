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
            //var test = new SendEmails();
            //test.Send();

            CrudOperations operations = new CrudOperations();
            operations.Menu();
        }
    }
}
