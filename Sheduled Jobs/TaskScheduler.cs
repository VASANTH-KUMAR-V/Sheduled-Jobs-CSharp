using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheduled_Jobs
{
    static class TaskScheduler
    {
        public static void Shedule()
        {
            // Create and open a file for writing
            StreamWriter ourStream = File.CreateText(@"C:\Temp\test.txt");

            // Write text to the file
            ourStream.WriteLine("This is a task schedule text file created at " + DateTime.Now);

            // Close the stream
            ourStream.Close();

            // Output a confirmation message
            Console.WriteLine("File created successfully");
        }
    }
}
