namespace GuestlineLabs.App.Repositories
{
    public interface IBookingRepository
    {
        int GetBookingCount(string hotelId, string roomType, DateTime startDate, DateTime endDate);
    }
}
