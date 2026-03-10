using HMS_BO;
using Microsoft.Data.SqlClient;

namespace HMS_DAL
{
    public class AppointmentDAL
    {
        public void BookAppointmentToDatabase(int doctorID, string conic, DateTime date)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Insert into Appointment (DoctorID,PatientCNIC,AppointmentDate) values (@DoctorID,@PatientCNIC,@AppointmentDate)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    cmd.Parameters.AddWithValue("@PatientCNIC", conic);
                    cmd.Parameters.AddWithValue("@AppointmentDate", date);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void CancelAppointment(int appointmentID, string cnic)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "Delete from Appointment WHERE AppointmentID=@AppointmentID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void GetMostConsultedDoctor()
        {
            using (SqlConnection con=DbHelper.GetConnection())
            {
                con.Open();

                string query = @"Select TOP 1 WITH TIES d.DoctorID,d.Name,
               COUNT(a.AppointmentID) AS TotalAppointments
            FROM Appointment a
            INNER JOIN Doctor d ON a.DoctorID = d.DoctorID
            GROUP BY d.DoctorID, d.Name
            ORDER BY TotalAppointments DESC;";

                using (var cmd = new SqlCommand(query, con))
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                    {
                        Console.WriteLine("No appointments found");
                        return;
                    }
                    Console.WriteLine("Most Consulted Doctor found:");
                    while (dr.Read())
                    {
                        int doctorId = dr.GetInt32(0);
                        string name = dr.GetString(1);
                        int total = dr.GetInt32(2);

                        Console.WriteLine($"ID={doctorId}, Name={name}, Appointments={total}");
                    }
                }
            }
        }

        public int CountAppointmentsByDoctor(int doctorID)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Appointment WHERE DoctorID = @DoctorID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
