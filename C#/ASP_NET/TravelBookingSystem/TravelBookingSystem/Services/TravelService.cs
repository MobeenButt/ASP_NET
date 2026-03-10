using Microsoft.Data.SqlClient;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services
{
    public class TravelService:ITravelService
    {
        public List<TravelPackage> GetAllPackages()
        {
            var list = new List<TravelPackage>();
            string query = "Select * from TravelPackages";
            using(SqlConnection con=DbHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    int PackageID = reader.GetInt32(0);
                    string PackageName = reader.GetString(1);
                    string Destination = reader.GetString(2);
                    int DurationDays = reader.GetInt32(3);
                    decimal Price = reader.GetDecimal(4);
                    int AvailableSeats = reader.GetInt32(5);
                    DateTime DepartureDate=reader.GetDateTime(6);
                    TravelPackage package = new TravelPackage
                    {
                        PackageID = PackageID,
                        PackageName = PackageName,
                        Destination = Destination,
                        DurationDays = DurationDays,
                        Price = Price,
                        AvailableSeats = AvailableSeats,
                        DepartureDate=DepartureDate
                    };
                    list.Add(package);
                }
            }
            return list;
        }
        public TravelPackage GetPackageById(int id)
        {
            string query = "Select PackageId, PackageName, Destination, Duration,Price,AvailableSeats,DepartureDate from TravelPackages Where PackageId =@id ";
            using(SqlConnection con = DbHelper.GetConnection()) {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    int PackageID = reader.GetInt32(0);
                    string PackageName = reader.GetString(1);
                    string Destination = reader.GetString(2);
                    int DurationDays = reader.GetInt32(3);
                    decimal Price = reader.GetDecimal(4);
                    int AvailableSeats = reader.GetInt32(5);
                    DateTime DepartureDate = reader.GetDateTime(6);
                    TravelPackage package = new TravelPackage
                    {
                        PackageID = PackageID,
                        PackageName = PackageName,
                        Destination = Destination,
                        DurationDays = DurationDays,
                        Price = Price,
                        AvailableSeats = AvailableSeats,
                        DepartureDate = DepartureDate
                    };
                    return package;
                }
                else
                {
                    return null;
                }
            }
        }
       public bool CreateBooking(Booking booking)
        {
            //validation for bookings
            if(booking==null)
            {
                throw new ArgumentNullException(nameof(booking));            }
            //Package check available or not 
            var package = GetPackageById(booking.PackageID);
            if (package == null)
            {
                throw new ArgumentException("Package not Found");
            }
            if(package.AvailableSeats<booking.NumberOfTravelers)
            {
               throw new ArgumentException("Not enough available seats for the selected package.");
            }
            //Insert booking
           
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string insertQuery = "Insert into Bookings (PackageID,CustomerName,CustomerEmail,NumberOfTravelers,TotalAmount,BookingDate) Values (@PackageID,@CustomerName,@CustomerEmail,@NumberOfTravelers,@TotalAmount,@BookingDate)";
                SqlCommand cmd = new SqlCommand(insertQuery,con);
                cmd.Parameters.AddWithValue("@PackageID", booking.PackageID);
                cmd.Parameters.AddWithValue("@CustomerName", booking.CustomerName);
                cmd.Parameters.AddWithValue("@CustomerEmail", booking.CustomerEmail);
                cmd.Parameters.AddWithValue("@NumberOfTravelers", booking.NumberOfTravelers);
                cmd.Parameters.AddWithValue("@TotalAmount", booking.TotalAmount);
                cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                cmd.ExecuteNonQuery();
            }
            //update seats
            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                string updateQuery = "Update TravelPackages Set AvailableSeats=AvailableSeats - @NumberOfTravelers Where PackageID=@PackageID";
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@NumberOfTravelers", booking.NumberOfTravelers);
                cmd.Parameters.AddWithValue("@PackageID", booking.PackageID);
                cmd.ExecuteNonQuery();
            }
            return true;
        }
    }
}
