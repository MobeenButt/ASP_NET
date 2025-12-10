using Travel_Booking_System_with_Identity.Models;

namespace Travel_Booking_System_with_Identity.Services
{
    public interface ITravelService
    {
        List<TravelPackage> GetAllPackages();
        TravelPackage? GetPackageById(int packageId);
        List<Booking> GetBookingsByUser(string userEmail);
        List<Booking> GetAllBookings();
        Booking CreateBooking(Booking booking);
        void UpdateBookingStatus(int bookingId, string status);
        void SeedSamplePackages();
    }
}
