using GuestlineLabs.App.Queries;
using GuestlineLabs.App.Repositories;

namespace GuestlineLabs.App.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingRepository _bookingRepository;

        public AvailabilityService(IHotelRepository hotelRepository, IBookingRepository bookingRepository)
        {
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }

        public int CheckAvailability(AvailabilityQuery query)
        {
            var totalRooms = _hotelRepository.GetRoomCount(query.HotelId, query.RoomType);
            var bookedRooms = _bookingRepository.GetBookingCount(query.HotelId, query.RoomType, query.StartDate, query.EndDate);

            return totalRooms - bookedRooms;
        }
    }
}
