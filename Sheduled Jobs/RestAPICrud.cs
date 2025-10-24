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
                    case 2:
                        RemovePatientsAsync().Wait();
                        break;
                    case 3:
                        ShowPatientsAsync().Wait();
                        break;
                    case 4:
                        UpdatePatientsAsync().Wait();
                        break;
                    case 5:
                        SearchPatientsNameAsync().Wait();
                        break;
                    case 6:
                        SearchPatientsEmailAsync().Wait();
                        break;
                    //case 7:
                    //    SearchByMobile();
                    //    break;
                    //case 8:
                    //// Console.WriteLine("hai");
                    case 9:
                        SearchPatientsLocationAsync().Wait();
                        break;
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
                ShowPatients(patients);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching or displaying patients: " + ex.Message);
            }
        }

        private void ShowPatients(List<PatientDetails> patients)
        {
            if (patients == null || patients.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("There are no patient details to show. Please enter 1 to add patient details.\n");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------------ABC Hospitals----------------------------------------\n");
            Console.WriteLine("------------------------------------Patient Details---------------------------------------\n");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Age",-5} {"Mobile",-15} {"Email",-25} {"Location",-15}");
            Console.WriteLine(new string('-', 90));

            foreach (var patient in patients)
            {
                Console.WriteLine(
                    $"{patient.Id,-5} {patient.Name,-20} {patient.Age,-5} {patient.Mobile,-15} {patient.Email,-25} {patient.Location,-15}");
            }
        }

        public async Task RemovePatientsAsync()
        {
            try
            {
                Console.Write("Enter Patient ID to remove: ");
                if (long.TryParse(Console.ReadLine(), out long id))
                {

                    var endpoint = $"api/JsonCRUD/Delete/{id}";
                    var response = await _apiClient.DeleteDataAsync(endpoint);
                    Console.WriteLine("Patient removed successfully via REST API.");
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid integer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing patient via REST API: {ex.Message}");
            }
        }

        public async Task UpdatePatientsAsync()
        {
            try
            {
                Console.Write("Enter Patient ID to Update: ");
                if (long.TryParse(Console.ReadLine(), out long id))
                {
                    var patient = new PatientDetails();
                    Console.Write("Enter the Name ");
                    string name = Console.ReadLine();
                    patient.Name = name;
                    Console.Write("Enter the Age");
                    string age = Console.ReadLine();
                    if (int.TryParse(age, out int a))
                        patient.Age = a;
                    Console.Write("Enter the Location");
                    string location = Console.ReadLine();
                    patient.Location = location;
                    var endpoint = $"api/JsonCRUD/Update/{id}";
                    var response = await _apiClient.PutDataAsync(endpoint, patient);
                    Console.WriteLine("Patient Updated successfully via REST API.");
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid integer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing patient via REST API: {ex.Message}");
            }
        }

        public async Task SearchPatientsNameAsync()
        {
            try
            {
                Console.Write("Enter Patient Name: ");
                string name = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid name. Please enter a valid patient name.");
                    return;
                }

                // Encode the name to handle spaces/special characters
                var endpoint = $"/api/JsonCRUD/SearchPatientByName?name={Uri.EscapeDataString(name)}";
                var response = await _apiClient.GetDataAsync(endpoint);

                if (string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine("No data returned from the API.");
                    return;
                }

                // Deserialize JSON with case-insensitive property names
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var patients = System.Text.Json.JsonSerializer.Deserialize<List<PatientDetails>>(response, options);

                if (patients != null && patients.Count > 0)
                {
                    Console.WriteLine($"\nFound {patients.Count} patient(s) matching \"{name}\":\n");
                    ShowPatients(patients);
                }
                else
                {
                    Console.WriteLine($"No patients found with the name containing \"{name}\".");
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching patients via REST API: {ex.Message}");
            }
        }

        public async Task SearchPatientsLocationAsync()
        {
            try
            {
                Console.Write("Enter Patient Location: ");
                string location = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(location))
                {
                    Console.WriteLine("Invalid name. Please enter a valid patient Location.");
                    return;
                }

                // Encode the name to handle spaces/special characters
                var endpoint = $"/api/JsonCRUD/SearchPatientByLocation?location={Uri.EscapeDataString(location)}";
                var response = await _apiClient.GetDataAsync(endpoint);

                if (string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine("No data returned from the API.");
                    return;
                }

                // Deserialize JSON with case-insensitive property names
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var patients = System.Text.Json.JsonSerializer.Deserialize<List<PatientDetails>>(response, options);

                if (patients != null && patients.Count > 0)
                {
                    Console.WriteLine($"\nFound {patients.Count} patient(s) matching \"{location}\":\n");
                    ShowPatients(patients);
                }
                else
                {
                    Console.WriteLine($"No patients found with the name containing \"{location}\".");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error searching patients via REST API: {ex.Message}");
            }
        }

        public async Task SearchPatientsEmailAsync()
        {
            try
            {
                Console.Write("Enter Patient Email: ");
                string email = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Invalid name. Please enter a valid patient Email.");
                    return;
                }

                // Encode the name to handle spaces/special characters
                var endpoint = $"/api/JsonCRUD/SearchPatientByEmail?email={Uri.EscapeDataString(email)}";
                var response = await _apiClient.GetDataAsync(endpoint);

                if (string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine("No data returned from the API.");
                    return;
                }

                // Deserialize JSON with case-insensitive property names
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var patients = System.Text.Json.JsonSerializer.Deserialize<List<PatientDetails>>(response, options);

                if (patients != null && patients.Count > 0)
                {
                    Console.WriteLine($"\nFound {patients.Count} patient(s) matching \"{email}\":\n");
                    ShowPatients(patients);
                }
                else
                {
                    Console.WriteLine($"No patients found with the name containing \"{email}\".");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error searching patients via REST API: {ex.Message}");
            }
        }



    }
}
