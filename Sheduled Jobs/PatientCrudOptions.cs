using PatientLibrary;
using System;

namespace PatientAppUsingJson
{
    public class CrudOperations
    {
        private readonly PatientManager manager = new PatientManager();

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Patient Management ====");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Remove Patient");
                Console.WriteLine("3. Update Patient");
                Console.WriteLine("4. View All Patients");
                Console.WriteLine("5. Search Patient by Name");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input!");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddPatient();
                        break;
                    case 2:
                        RemovePatient();
                        break;
                    case 3:
                        UpdatePatient();
                        break;
                    case 4:
                        ViewPatients();
                        break;
                    case 5:
                        SearchByName();
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }

        private void AddPatient()
        {
            var patient = new Patient();

            Console.Write("Name: ");
            patient.Name = Console.ReadLine();
            Console.Write("Mobile: ");
            patient.Mobile = long.Parse(Console.ReadLine());
            Console.Write("Age: ");
            patient.Age = int.Parse(Console.ReadLine());
            Console.Write("Email: ");
            patient.Email = Console.ReadLine();
            Console.Write("Location: ");
            patient.Location = Console.ReadLine();

            manager.AddPatient(patient);
            Console.WriteLine("Patient added successfully.");
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
                    if (!string.IsNullOrWhiteSpace(name)) p.Name = name;

                    Console.Write("New Mobile (leave blank to skip): ");
                    string mobile = Console.ReadLine();
                    if (long.TryParse(mobile, out long m)) p.Mobile = m;

                    Console.Write("New Age (leave blank to skip): ");
                    string age = Console.ReadLine();
                    if (int.TryParse(age, out int a)) p.Age = a;

                    Console.Write("New Email (leave blank to skip): ");
                    string email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email)) p.Email = email;

                    Console.Write("New Location (leave blank to skip): ");
                    string location = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(location)) p.Location = location;
                });

                Console.WriteLine(result ? "Patient updated." : "Patient not found.");
            }
        }

        private void ViewPatients()
        {
            var patients = manager.GetAllPatients();
            Console.WriteLine("\n--- Patient List ---");
            foreach (var p in patients)
            {
                Console.WriteLine($"{p.Id}: {p.Name}, {p.Mobile}, {p.Email}, {p.Age}, {p.Location}");
            }
        }

        private void SearchByName()
        {
            Console.Write("Enter name to search: ");
            string search = Console.ReadLine().ToLower();
            var matches = manager.Search(p => p.Name.ToLower().Contains(search));

            Console.WriteLine("\n--- Search Results ---");
            foreach (var p in matches)
            {
                Console.WriteLine($"{p.Id}: {p.Name}, {p.Mobile}, {p.Email}, {p.Age}, {p.Location}");
            }
        }
    }
}
