using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DbPatientLibrary
{
    public class PatientRepository
    {
        public string connectionString = "Server=DESKTOP-04RCHH9;Database=Hospital;Integrated Security=True;";
        public List<Patient> GetPatients()
        {
            try
            {
                
                string sql = $"select * from PatientDetails";

                var connection = new SqlConnection(connectionString);
                connection.Open();
                var result = connection.Query<Patient>(sql).ToList();
                connection.Close();

                return result;

            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Patient GetPatientById(long id)
        {
            try
            {
                string sql = "SELECT * FROM PatientDetails WHERE Patient_Id = @Id";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var patient = connection.QueryFirstOrDefault<Patient>(sql, new { Id = id });
                    return patient;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InsertPatients(PatientDetails record)
        {
            try
            {
                string sql = $"insert into PatientDetails values('{record.Name}', '{record.Email}','{record.Mobile}', {record.Age},'{record.Location}')";

                var connection = new SqlConnection(connectionString);
                connection.Open();
                var result = connection.Execute(sql);
                connection.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeletePatients(long id)
        {
            try
            {
                string sql = $"DELETE FROM PatientDetails WHERE Patient_Id = @ID";
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var result = connection.Execute(sql, new { Id = id });
                connection.Close();
                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdatePatient(Patient patient)
        {
            try
            {
                string sql = @" UPDATE PatientDetails 
                SET 
                Name = @Name,
                Age = @Age,
                Location = @Location
                WHERE Patient_Id = @Id";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int rowsAffected = connection.Execute(sql, new
                    {
                        Id = patient.Patient_Id,
                        Name = patient.Name,
                        Age = patient.Age,
                        Location = patient.Location
                    });
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
        }

        //public List<Patient> SearchPatients(Patient patient)
        //{
        //    try
        //    {
        //        string sql = @"
        //SELECT * FROM PatientDetails
        //WHERE
        //    (@Name IS NULL OR name LIKE '%' + @Name + '%') 
        //    AND (@Email IS NULL OR email LIKE '%' + @Email + '%')
        //    AND (@Location IS NULL OR location LIKE '%' + @Location + '%')
        //";

        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            var result = connection.Query<Patient>(sql, new
        //            {
        //                Name = string.IsNullOrWhiteSpace(patient.Name) ? null : patient.Name,
        //                Email = string.IsNullOrWhiteSpace(patient.Email) ? null : patient.Email,
        //                Location = string.IsNullOrWhiteSpace(patient.Location) ? null : patient.Location
        //            }).ToList();

        //            return result;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("SQL Error: " + ex.Message);
        //        return new List<Patient>();
        //    }
        //}





    }
}
