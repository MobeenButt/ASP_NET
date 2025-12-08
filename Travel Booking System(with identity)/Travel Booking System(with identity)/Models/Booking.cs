using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Travel_Booking_System_with_identity_.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int PackageID { get; set; }
        public TravelPackage? Package { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser? Customer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int NumberOfTravelers { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
      

        public string BookingStatus { get; set; } = "Pending";
    }
}
