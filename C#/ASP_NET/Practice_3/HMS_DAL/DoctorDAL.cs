using HMS_BO;
using Microsoft.Data.SqlClient;

namespace HMS_DAL
{
    public class DoctorDAL
    {
        public Doctor_BO doctor;
        public void AddDoctorToFile()
        {
            string filepath = "doctor.txt";
        }

        public void AddDoctorToDatabase(Doctor_BO doctor)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Insert into Doctor (Name,Specialization,IsAvailable) Values (@Name,@Specialization,@IsAvailable)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", doctor.GetName);
                cmd.Parameters.AddWithValue("@Specialization", doctor.GetSpecialization);
                cmd.Parameters.AddWithValue("@IsAvailable", doctor.IsAvailable);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateDoctorToDatabase(int id, string name, string specialization)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "UPDATE Doctor SET Name=@name, Specialization=@specialization WHERE DoctorID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@specialization", specialization);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteDoctorToDatabase(int id)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Delete from Doctor WHERE DoctorID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Doctor_BO> DisplayDoctor()
        {
            List<Doctor_BO> list = new List<Doctor_BO>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Select * from Doctor";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Doctor_BO doctor = new Doctor_BO();
                    doctor.GetDoctorID = reader.GetInt32(0);
                    doctor.GetName = reader.GetString(1);
                    doctor.GetSpecialization = reader.GetString(2);
                    doctor.IsAvailable = reader.GetBoolean(3);
                    list.Add(doctor);
                }
            }
            return list;
        }

    }
}
