namespace Travel_Booking_System_with_identity_.Models.ViewModels
{
    public class UserDashboardViewModel
    {
        public List<TravelPackage> Packages { get; set; } = new();
        public List<Booking> Bookings { get; set; } = new();
    }
}
