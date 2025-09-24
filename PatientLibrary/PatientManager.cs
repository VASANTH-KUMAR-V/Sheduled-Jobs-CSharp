using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PatientLibrary
{
    public class PatientManager
    {
        private readonly string filePath = Path.Combine("Data", "data.json");
        private List<Patient> patients;

        public PatientManager()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory("Data");
                File.WriteAllText(filePath, "[]");
            }

            var json = File.ReadAllText(filePath);
            patients = JsonConvert.DeserializeObject<List<Patient>>(json) ?? new List<Patient>();
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(patients, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void AddPatient(Patient patient)
        {
            patient.Id = patients.Any() ? patients.Max(p => p.Id) + 1 : 1;
            patients.Add(patient);
            SaveData();
        }

        public bool RemovePatient(int id)
        {
            var patient = patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                patients.Remove(patient);
                SaveData();
                return true;
            }
            return false;
        }

        public bool UpdatePatient(int id, Action<Patient> updateAction)
        {
            var patient = patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                updateAction(patient);
                SaveData();
                return true;
            }
            return false;
        }

        public List<Patient> GetAllPatients() => patients;

        public List<Patient> Search(Func<Patient, bool> predicate)
        {
            return patients.Where(predicate).ToList();
        }
    }
}
