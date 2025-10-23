using System;
using System.Threading.Tasks;
using Models;
using RestAPIClient; // <-- Reference to your reusable client

namespace Sheduled_Jobs
{
    public class RestApiEmailSender
    {
        private readonly MyApiClient _apiClient;
        private readonly string _endpoint = "api/SendEmail";

        public RestApiEmailSender()
        {
            // You can use your actual API URL here
            _apiClient = new MyApiClient("https://localhost:44303/");
        }

        public async Task SendAsync()
        {
            try
            {
                Console.WriteLine("\n=== Send Email Using REST API ===");

                var email = new Email();

                Console.Write("Enter From Address (Gmail): ");
                email.FromAddress = Console.ReadLine();

                Console.Write("Enter To Address: ");
                email.ToAddress = Console.ReadLine();

                Console.Write("Enter CC Address (optional): ");
                email.CcAddress = Console.ReadLine();

                Console.Write("Enter BCC Address (optional): ");
                email.BccAddress = Console.ReadLine();

                Console.Write("Enter Subject: ");
                email.Subject = Console.ReadLine();

                Console.Write("Enter Content: ");
                email.Content = Console.ReadLine();

                Console.Write("Enter Gmail App Password: ");
                email.GmailAppPassword = Console.ReadLine();

                Console.WriteLine("\nSending email request to API...");

                // 🔥 Use MyApiClient to send POST request
                var response = await _apiClient.PostDataAsync(_endpoint, email);

                Console.WriteLine("Email sent successfully via REST API!");
                Console.WriteLine("Response: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email via REST API: {ex.Message}");
            }
        }
    }
}
