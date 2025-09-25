using Org.BouncyCastle.Asn1.Pkcs;
using PatientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientAppUsingJson
{
    public class CrudOperations
    {
        private readonly PatientManager manager = new PatientManager();

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
                Console.WriteLine("8 - Search by Age");
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
                        AddPatient();
                        break;
                    case 2:
                        RemovePatient();
                        break;
                    case 3:
                        ShowPatients();
                        break;
                    case 4:
                        UpdatePatient();
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        Console.WriteLine("Exiting.....");
                        return; // exit the method, breaking out of the while loop
                    default:
                        Console.WriteLine("You have entered an invalid number to perform an action.");
                        break;
                }
            }
        }


        private void AddPatient()
        {
            var patient = new Patient();

            Console.Write("Name: ");
            patient.Name = Console.ReadLine();
            Console.Write("Mobile: ");
            patient.Mobile = long.Parse(Console.ReadLine());
            Console.Write("Email: ");
            patient.Email = Console.ReadLine();
            var AllPatients = manager.GetAllPatients();
            if (AllPatients.Any(p => p.Mobile == patient.Mobile || p.Email == patient.Email))
            {
                Console.WriteLine("A patient with the same mobile or email already exists.");
                return; 
            }
            Console.Write("Age: ");
            patient.Age = int.Parse(Console.ReadLine());
            Console.Write("Location: ");
            patient.Location = Console.ReadLine();
            manager.AddPatient(patient);
            Console.WriteLine("Patient added successfully.");
        }

        private void ShowPatients()
        {
            var patients = manager.GetAllPatients();

            if (patients.Count != 0)
            {
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
            else
            {
                Console.WriteLine();
                Console.WriteLine("There are no patient details to show. Please enter 1 to add patient details.\n");
            }
        }

        private void RemovePatient()
        {
            Console.Write("Enter Patient ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool result = manager.RemovePatient(id);
                Console.WriteLine(result ? "Patient removed." : "Patient not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void UpdatePatient()
        {
            Console.Write("Enter Patient ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool result = manager.UpdatePatient(id, p =>
                {
                    Console.Write("New Name (leave blank to skip): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                        p.Name = name;

                    Console.Write("New Age (leave blank to skip): ");
                    string age = Console.ReadLine();
                    if (int.TryParse(age, out int a))
                        p.Age = a;

                    Console.Write("New Location (leave blank to skip): ");
                    string location = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(location))
                        p.Location = location;
                });

                Console.WriteLine(result ? "Patient updated." : "Patient not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }



    }
}
