using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CRUD_Operation_Using_JSON;
using Newtonsoft.Json;

namespace PatientLibrary
{
    public class PatientManager
    {
        private readonly string dataPath;
        private List<Patient> patients;
        private int idCounter;

        public PatientManager()
        {
            dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(dataPath))
            {
                string json = File.ReadAllText(dataPath);
                patients = JsonConvert.DeserializeObject<List<Patient>>(json) ?? new List<Patient>();
                idCounter = patients.Count > 0 ? patients.Max(p => p.Id) + 1 : 1;
            }
            else
            {
                patients = new List<Patient>();
                idCounter = 1;
            }
        }

        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(patients, Formatting.Indented);
            File.WriteAllText(dataPath, json);
        }

        public void AddPatient(Patient patient)
        {
            if (IsDuplicate(patient.Mobile, patient.Email))
            {
                throw new Exception("Duplicate mobile number or email.");
            }

            patient.Id = idCounter++;
            patients.Add(patient);
            SaveData();
        }

        public bool IsDuplicate(long mobile, string email)
        {
            return patients.Any(p => p.Mobile == mobile || p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool RemovePatient(int id)
        {
            var p = patients.FirstOrDefault(p => p.Id == id);
            if (p != null)
            {
                patients.Remove(p);
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

        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        public List<Patient> Search(Func<Patient, bool> predicate)
        {
            return patients.Where(predicate).ToList();
        }
    }
}
