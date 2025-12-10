using Travel_Booking_System_with_Identity.Models;

namespace Travel_Booking_System_with_Identity.Services
{
    public interface ITravelService
    {
      public  List<TravelPackage> GetAllPackages();
        public TravelPackage? GetPackageById(int packageId);
        public List<Booking> GetBookingsByUser(string userEmail);
        public List<Booking> GetAllBookings();
        public Booking CreateBooking(Booking booking);
        public void ChangeBookingStatus(int bookingId, string status);
        public void SeedSamplePackages();
    }
}
