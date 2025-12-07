using System.ComponentModel.DataAnnotations;

namespace Travel_Booking_System_with_identity_.Models.ViewModels
{
    public class BookPackageViewModel
    {
        [Required]
        public int PackageId { get; set; }
        
        [Required]
        public string? CustomerName { get; set; }
        [Required,EmailAddress]
        public string? CustomerEmail { get; set; }  
        [Required,Range(1,50)]
        public int NumberOfTravelers { get; set; }
    }
}
