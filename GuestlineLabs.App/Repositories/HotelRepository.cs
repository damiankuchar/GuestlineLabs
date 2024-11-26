using GuestlineLabs.App.Models;
using GuestlineLabs.App.Services;
using System.Text.Json;

namespace GuestlineLabs.App.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly List<Hotel> _hotels;

        public HotelRepository(IFileService fileService, string filePath)
        {
            if (!fileService.FileExists(filePath))
            {
                throw new FileNotFoundException($"Hotel file not found: {filePath}");
            }

            var fileContent = fileService.ReadFileAllText(filePath);

            _hotels = JsonSerializer.Deserialize<List<Hotel>>(fileContent, JsonOptionsService.Default) ?? [];
        }

        public Hotel GetHotelById(string hotelId)
        {
            var hotel = _hotels
                .FirstOrDefault(x => x.Id == hotelId);

            if (hotel == null)
            {
                throw new ArgumentException($"Hotel with ID {hotelId} not found.");
            }

            return hotel;
        }

        public int GetRoomCount(string hotelId, string roomType)
        {
            var hotel = GetHotelById(hotelId);
            
            var roomCount = hotel.Rooms.Count(x => x.RoomType == roomType);

            return roomCount;
        }
    }
}
