using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services
{
    public interface ITravelService
    {
        List<TravelPackage> GetAllPackages();
        TravelPackage GetPackageById(int id);
        bool CreateBooking(Booking booking);
    }
}
