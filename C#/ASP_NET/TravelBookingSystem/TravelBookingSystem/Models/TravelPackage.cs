namespace TravelBookingSystem.Models
{
    public class TravelPackage
    {
        public int PackageID { get; set; }
        public string PackageName { get; set; }
        public string Destination { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DepartureDate { get; set; }

    }
}
