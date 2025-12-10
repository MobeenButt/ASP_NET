using Microsoft.EntityFrameworkCore;
using Travel_Booking_System_with_Identity.Data;
using Travel_Booking_System_with_Identity.Models;

namespace Travel_Booking_System_with_Identity.Services
{
    public class TravelService : ITravelService
    {
        private readonly ApplicationDbContext dbContext;

        public TravelService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        Booking ITravelService.CreateBooking(Booking booking)
        {
            dbContext.Bookings.Add(booking);
            dbContext.SaveChanges();
            return booking;
        }

        List<Booking> ITravelService.GetAllBookings()
        {
            return dbContext.Bookings
                .Include(b => b.Package)
                .Include(b => b.CustomerName)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        List<TravelPackage> ITravelService.GetAllPackages()
        {
            return dbContext.TravelPackages.ToList();
        }

        List<Booking> ITravelService.GetBookingsByUser(string userEmail)
        {
            return dbContext.Bookings
                .Include(b => b.Package)
                .Where(b => b.CustomerEmail == userEmail)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        TravelPackage? ITravelService.GetPackageById(int packageId)
        {
            return dbContext.TravelPackages.Find(packageId);
        }

        void ITravelService.SeedSamplePackages()
        {
            if (!dbContext.TravelPackages.Any())
            {
                var packages = new List<TravelPackage>
                {
                    new TravelPackage
                    {
                        PackageName = "Paris Adventure",
                        Destination = "Paris, France",
                        Price = 1299.99m,
                        DurationDays = 5,
                        AvailableSeats = 20
                    },
                    new TravelPackage
                    {
                        PackageName = "Tokyo Experience",
                        Destination = "Tokyo, Japan",
                        Price = 1899.99m,
                        DurationDays = 7,
                        AvailableSeats = 15
                    },
                    new TravelPackage
                    {
                        PackageName = "Caribbean Cruise",
                        Destination = "Caribbean Islands",
                        Price = 2499.99m,
                        DurationDays = 10,
                        AvailableSeats = 30
                    }
                };

                dbContext.TravelPackages.AddRange(packages);
                dbContext.SaveChanges();
            }
        }

        void ITravelService.UpdateBookingStatus(int bookingId, string status)
        {
            var booking = dbContext.Bookings.Find(bookingId);
            if (booking != null)
            {
                booking.BookingStatus = status;
                dbContext.SaveChanges();
            }
        }
    }
}
