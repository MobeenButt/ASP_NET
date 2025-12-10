using System.ComponentModel.DataAnnotations;

namespace Travel_Booking_System_with_Identity.Models
{
    public class TravelPackage
    {
        [Key]
        public int PackageId { get; set; }
        [Required]
        public string PackageName { get; set; }
        public string Destination { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
