using DbPatientLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheduled_Jobs
{
    public class DbPatientCrudoperation
    {
        PatientRepository repo = new PatientRepository();
        public void Menu()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("\nDo you want to perform an action?");
                    Console.WriteLine("1 - Add Patient");
                    Console.WriteLine("2 - Remove Patient");
                    Console.WriteLine("3 - View Patient Details");
                    Console.WriteLine("4 - Update Patient Details");
                    Console.WriteLine("5 - Search ");
                    Console.WriteLine("6 - Exit");

                    if (!int.TryParse(Console.ReadLine(), out int nextAction))
                    {
                        Console.WriteLine("Invalid input! Please enter a number.");
                        continue;
                    }

                    switch (nextAction)
                    {
                        case 1:
                            AddPatient();
                            break;
                        case 2:
                            RemovePatient();
                            break;
                        case 3:
                            var patients = repo.GetPatients();
                            ShowPatients(patients);
                            break;
                        case 4:
                            UpdatePatient();
                            break;
                        case 5:
                            SearchPatient();
                            break;
                        case 6:
                            Console.WriteLine("Exiting.....");
                            return;
                        default: 
                            Console.WriteLine("You have entered an invalid number to perform an action.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }
        }

        private void ShowPatients(List<Patient> patients)
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
                    $"{patient.Patient_Id,-5} {patient.Name,-20} {patient.Age,-5} {patient.Mobile,-15} {patient.Email,-25} {patient.Location,-15}");
            }
        }

        private void AddPatient()
        {
            var patients = new Patient();
            Console.Write("Name: ");
            patients.Name = Console.ReadLine();
            Console.Write("Email: ");
            patients.Email = Console.ReadLine();
            Console.Write("Mobile: ");
            patients.Mobile = long.Parse(Console.ReadLine());
            var allPatients = repo.GetPatients();
            if (allPatients.Any(p => p.Mobile == patients.Mobile || p.Email == patients.Email))
            {
                Console.WriteLine("A patient with the same mobile or email already exists.");
                return;
            }
            Console.Write("Age: ");
            patients.Age = int.Parse(Console.ReadLine());
            Console.Write("Location: ");
            patients.Location = Console.ReadLine();
            repo.InsertPatients(patients);
            Console.WriteLine("Patient added successfully.");
        }

        private void RemovePatient()
        {
            Console.Write("Enter Patient ID to remove: ");

            if (long.TryParse(Console.ReadLine(), out long id))
            {
                bool result = repo.DeletePatients(id); // Ensure method name matches
                Console.WriteLine(result ? "Patient removed successfully." : "Patient not found or could not be removed.");
            }
            else
            {
                Console.WriteLine("Invalid ID entered. Please enter a numeric value.");
            }
        }

        private void UpdatePatient()
        {
            Console.Write("Enter Patient ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Step 1: Get the patient by ID
                var patient = repo.GetPatientById(id);  // <-- Use this method to fetch single patient
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    return;
                }

                // Step 2: Ask user for fields to update
                Console.Write("New Name (leave blank to skip): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    patient.Name = name;

                Console.Write("New Age (leave blank to skip): ");
                string ageInput = Console.ReadLine();
                if (int.TryParse(ageInput, out int age))
                    patient.Age = age;

                Console.Write("New Location (leave blank to skip): ");
                string location = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(location))
                    patient.Location = location;

                // Step 3: Save changes
                bool result = repo.UpdatePatient(patient);
                Console.WriteLine(result ? "Patient updated successfully." : "Update failed.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void SearchPatient()
        {
            Console.Write("Enter Patient Name (or leave blank): ");
            var name = Console.ReadLine();

            Console.Write("Enter Email (or leave blank): ");
            var email = Console.ReadLine();

            Console.Write("Enter Mobile (or leave blank): ");
            var mobileInput = Console.ReadLine();
            long mobile = 0;
            long.TryParse(mobileInput, out mobile);

            Console.Write("Enter Age (or leave blank): ");
            var ageInput = Console.ReadLine();
            int age = 0;
            int.TryParse(ageInput, out age);

            Console.Write("Enter Location (or leave blank): ");
            var location = Console.ReadLine();

            var searchCriteria = new Patient
            {
                Name = string.IsNullOrWhiteSpace(name) ? null : name,
                Email = string.IsNullOrWhiteSpace(email) ? null : email,
                Mobile = mobile,
                Age = age,
                Location = string.IsNullOrWhiteSpace(location) ? null : location
            };

            var matchedPatients = repo.SearchPatients(searchCriteria);

            if (matchedPatients.Any())
            {
                Console.WriteLine("Matched Patients:");
                ShowPatients(matchedPatients);
            }
            else
            {
                Console.WriteLine("No matching patients found.");
            }
        }







    }
}
