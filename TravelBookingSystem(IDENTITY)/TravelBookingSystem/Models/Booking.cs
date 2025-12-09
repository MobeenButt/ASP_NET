namespace TravelBookingSystem.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int PackageID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int NumberOfTravelers { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }= DateTime.Now;
    }
}
