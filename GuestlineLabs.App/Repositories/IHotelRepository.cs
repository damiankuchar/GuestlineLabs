using GuestlineLabs.App.Models;

namespace GuestlineLabs.App.Repositories
{
    public interface IHotelRepository
    {
        Hotel GetHotelById(string hotelId);
        int GetRoomCount(string hotelId, string roomType);
    }
}
