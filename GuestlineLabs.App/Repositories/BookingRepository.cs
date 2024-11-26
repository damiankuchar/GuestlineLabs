using GuestlineLabs.App.Models;
using GuestlineLabs.App.Services;
using System.Text.Json;

namespace GuestlineLabs.App.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings;

        public BookingRepository(IFileService fileService, string filePath)
        {
            if (!fileService.FileExists(filePath))
            {
                throw new FileNotFoundException($"Booking file not found: {filePath}");
            }

            var fileContent = fileService.ReadFileAllText(filePath);

            _bookings = JsonSerializer.Deserialize<List<Booking>>(fileContent, JsonOptionsService.Default) ?? [];
        }

        public int GetBookingCount(string hotelId, string roomType, DateTime startDate, DateTime endDate)
        {
            var bookings = _bookings
                .Where(x =>
                    x.HotelId == hotelId &&
                    x.RoomType == roomType &&
                    !(x.DepartureDate <= startDate || x.ArrivalDate > endDate))
                .ToList();

            return bookings.Count;
        }
    }
}
