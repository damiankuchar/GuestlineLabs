namespace GuestlineLabs.App.Models
{
    public class Booking
    {
        public string HotelId { get; set; } = string.Empty;
        public string Arrival { get; set; } = string.Empty;
        public string Departure { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public string RoomRate { get; set; } = string.Empty;

        public DateTime ArrivalDate => DateTime.ParseExact(Arrival, "yyyyMMdd", null);
        public DateTime DepartureDate => DateTime.ParseExact(Departure, "yyyyMMdd", null);
    }
}
