using System;
using CRUD_Operation_Using_JSON;
using PatientLibrary;

class Program
{
    static void Main()
    {
        var manager = new PatientManager();

        while (true)
        {
            Console.WriteLine("\n1. Add\n2. Remove\n3. Update\n4. View\n5. Search\n6. Exit");
            Console.Write("Choose: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var p = new Patient();
                    Console.Write("Name: "); p.Name = Console.ReadLine();
                    Console.Write("Mobile: "); p.Mobile = long.Parse(Console.ReadLine());
                    Console.Write("Email: "); p.Email = Console.ReadLine();
                    Console.Write("Age: "); p.Age = int.Parse(Console.ReadLine());
                    Console.Write("Location: "); p.Location = Console.ReadLine();

                    try
                    {
                        manager.AddPatient(p);
                        Console.WriteLine("Patient added!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case 2:
                    Console.Write("Enter ID to remove: ");
                    int id = int.Parse(Console.ReadLine());
                    if (manager.RemovePatient(id))
                        Console.WriteLine("Removed.");
                    else
                        Console.WriteLine("Not found.");
                    break;

                case 3:
                    Console.Write("Enter ID to update: ");
                    int uid = int.Parse(Console.ReadLine());
                    manager.UpdatePatient(uid, patient =>
                    {
                        Console.Write("New Name (leave blank to skip): ");
                        var name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            patient.Name = name;
                    });
                    break;

                case 4:
                    var all = manager.GetAllPatients();
                    Console.WriteLine("Patients:");
                    foreach (var patient in all)
                    {
                        Console.WriteLine($"{patient.Id} - {patient.Name} - {patient.Mobile} - {patient.Email}");
                    }
                    break;

                case 5:
                    Console.Write("Search Name: ");
                    string search = Console.ReadLine().ToLower();
                    var matches = manager.Search(pat => pat.Name.ToLower().Contains(search));
                    foreach (var pat in matches)
                    {
                        Console.WriteLine($"{pat.Id} - {pat.Name}");
                    }
                    break;

                case 6:
                    return;
            }
        }
    }
}
