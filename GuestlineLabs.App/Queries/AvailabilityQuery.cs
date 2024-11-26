namespace GuestlineLabs.App.Queries
{
    public class AvailabilityQuery
    {
        public string HotelId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string RoomType { get; private set; }

        private AvailabilityQuery(string hotelId, DateTime startDate, DateTime endDate, string roomType)
        {
            HotelId = hotelId;
            StartDate = startDate;
            EndDate = endDate;
            RoomType = roomType;
        }

        public static AvailabilityQuery Parse(string input)
        {
            var queryParts = input.Split(['(', ')', ',']);

            if (queryParts.Length < 3 || queryParts[0] != "Availability")
            {
                throw new FormatException("Invalid query format. Use: Availability(hotelId, dateRange, roomType)");
            }

            var hotelId = queryParts[1].Trim();
            var dateRange = queryParts[2].Trim();
            var roomType = queryParts[3].Trim();

            var (startDate, endDate) = ParseDateRange(dateRange);

            return new AvailabilityQuery(hotelId, startDate, endDate, roomType);
        }

        private static (DateTime startDate, DateTime endDate) ParseDateRange(string dateRange)
        {
            try
            {
                if (dateRange.Contains('-'))
                {
                    var parts = dateRange.Split('-');
                    return (DateTime.ParseExact(parts[0], "yyyyMMdd", null), DateTime.ParseExact(parts[1], "yyyyMMdd", null));
                }
                else
                {
                    var date = DateTime.ParseExact(dateRange, "yyyyMMdd", null);
                    return (date, date);
                }
            }
            catch
            {
                throw new FormatException("Invalid date range format. Use yyyyMMdd or yyyyMMdd-yyyyMMdd.");
            }
        }
    }
}
