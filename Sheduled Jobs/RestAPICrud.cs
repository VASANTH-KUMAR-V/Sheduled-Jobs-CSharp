using RestAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Sheduled_Jobs
{
    public class RestAPICrud
    {
        private readonly MyApiClient _apiClient; // Change to your API
        private const string _endpoint = "api/JsonCRUD"; // Adjust if needed // Adjust if your controller route is different

        public RestAPICrud()
        {
            // You can use your actual API URL here
            _apiClient = new MyApiClient("https://localhost:44303/");
        }
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nDo you want to perform an action?");
                Console.WriteLine("1 - Add Patient");
                Console.WriteLine("2 - Remove Patient");
                Console.WriteLine("3 - View Patient Details");
                Console.WriteLine("4 - Update Patient Details");
                Console.WriteLine("5 - Search by Name");
                Console.WriteLine("6 - Search by Email");
                Console.WriteLine("7 - Search by Mobile");
                //   Console.WriteLine("8 - Search by Age");
                Console.WriteLine("9 - Search by Location");
                Console.WriteLine("10 - Exit");

                if (!int.TryParse(Console.ReadLine(), out int nextAction))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                switch (nextAction)
                {
                    case 1:
                        AddingPatient().Wait();
                        break;
                    //case 2:
                    //    RemovePatientAsync();
                    //    break;
                    case 3:
                        ShowPatientsAsync().Wait();
                        break;
                    //case 4:
                    //    UpdatePatient();
                    //    break;
                    //case 5:
                    //    SearchByName();
                    //    break;
                    //case 6:
                    //    SearchByEmail();
                    //    break;
                    //case 7:
                    //    SearchByMobile();
                    //    break;
                    //case 8:
                    //// Console.WriteLine("hai");
                    //case 9:
                    //    SearchByLocation();
                    //    break;
                    case 10:
                        Console.WriteLine("Exiting.....");
                        return;
                    default:
                        Console.WriteLine("You have entered an invalid number to perform an action.");
                        break;
                }
            }
        }

        public async Task AddingPatient()
        {
            try
            {
                var patient = new PatientDetails();

                Console.Write("Name: ");
                patient.Name = Console.ReadLine();

                Console.Write("Mobile: ");
                if (!long.TryParse(Console.ReadLine(), out long mobile))
                {
                    Console.WriteLine("Invalid mobile number input. Please enter digits only.");
                    return;
                }
                patient.Mobile = mobile;

                Console.Write("Email: ");
                patient.Email = Console.ReadLine();

                Console.Write("Age: ");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine("Invalid age input. Please enter a valid number.");
                    return;
                }
                patient.Age = age;

                Console.Write("Location: ");
                patient.Location = Console.ReadLine();

                var response = await _apiClient.PostDataAsync(_endpoint, patient);


                if (response.StartsWith("Conflict:"))
                {
                    Console.WriteLine("Patient NOT added. Reason: " + response);
                }
                else if (response.StartsWith("Error"))
                {
                    Console.WriteLine("Failed to add patient. " + response);
                }
                else
                {
                    Console.WriteLine("Patient added successfully via REST API!");
                    Console.WriteLine("Response: " + response);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding patient via REST API: {ex.Message}");
            }
        }

        public async Task ShowPatientsAsync()
        {
            try
            {
                string jsonResponse = await _apiClient.GetDataAsync(_endpoint);
                List<PatientDetails> patients = JsonConvert.DeserializeObject<List<PatientDetails>>(jsonResponse);

                if (patients == null || patients.Count == 0)
                {
                    Console.WriteLine("\nThere are no patient details to show.\n");
                    return;
                }

                Console.WriteLine("\n-------------------------------------ABC Hospitals----------------------------------------\n");
                Console.WriteLine("------------------------------------Patient Details---------------------------------------\n");
                Console.WriteLine($"{"ID",-5} {"Name",-20} {"Age",-5} {"Mobile",-15} {"Email",-25} {"Location",-15}");
                Console.WriteLine(new string('-', 90));

                foreach (var patient in patients)
                {
                    Console.WriteLine($"{patient.Id,-5} {patient.Name,-20} {patient.Age,-5} {patient.Mobile,-15} {patient.Email,-25} {patient.Location,-15}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching or displaying patients: " + ex.Message);
            }
        }
    }
}
