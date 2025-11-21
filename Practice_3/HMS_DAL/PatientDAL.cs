using HMS_BO;
using Microsoft.Data.SqlClient;

namespace HMS_DAL
{
   
    public class PatientDAL
    {
        public Patient_BO patient;        
        public void AddPatientToFile()
        {
            string filepath = "patient.txt";
        }

        public void AddPatientToDatabase(Patient_BO patient)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Insert into Patient (Name,CNIC) Values (@name,@cnic)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", patient.NAME);
                cmd.Parameters.AddWithValue("@cnic", patient.CNIC);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdatePatientToDatabase(string cnic,string name)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Update Patient set cnic=@cnic, name=@name WHERE cnic=@cnic";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cnic", cnic);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeletePatientToDatabase(string cnic)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Delete from Patient WHERE cnic=@cnic";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cnic", cnic);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Patient_BO> DisplayPatient()
        {
            List<Patient_BO> list = new List<Patient_BO>();
            using (SqlConnection con= DbHelper.GetConnection())
            {
                con.Open();
                string query = "Select * from Patient";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Patient_BO patient = new Patient_BO(reader["Name"].ToString(), reader["CNIC"].ToString());
                    list.Add(patient);
                }
            }
            return list;
        }

    }
}
