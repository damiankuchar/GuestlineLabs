using GuestlineLabs.App.Queries;
using GuestlineLabs.App.Services;

namespace GuestlineLabs.App
{
    public class Application
    {
        private readonly IAvailabilityService _availabilityService;

        public Application(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        public void Run()
        {
            Console.WriteLine("Enter queries (empty line to exit):");

            while (true)
            {
                var userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    break;
                }

                try
                {
                    var query = AvailabilityQuery.Parse(userInput);
                    int availability = _availabilityService.CheckAvailability(query);
                    Console.WriteLine($"Availability for {query.RoomType} in {query.HotelId} from {query.StartDate:yyyy-MM-dd} to {query.EndDate:yyyy-MM-dd}: {availability}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
