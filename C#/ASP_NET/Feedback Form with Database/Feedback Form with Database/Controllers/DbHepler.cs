using Microsoft.Data.SqlClient;

namespace Feedback_Form_with_Database.Controllers
{
    public class DbHepler
    {
        public static string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=Feedback;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        public static SqlConnection GetConnectionString()
        {
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}
