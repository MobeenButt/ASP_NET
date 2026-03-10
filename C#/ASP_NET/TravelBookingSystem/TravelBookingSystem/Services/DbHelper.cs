using Microsoft.Data.SqlClient;
namespace TravelBookingSystem.Services
{
    public class DbHelper
    {
        public static string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=TravelBookingDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
