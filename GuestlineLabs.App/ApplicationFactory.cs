using GuestlineLabs.App.Repositories;
using GuestlineLabs.App.Services;

namespace GuestlineLabs.App
{
    public class ApplicationFactory
    {
        private const string DefaultHotelsFilePath = "Data/hotels.json";
        private const string DefaultBookingsFilePath = "Data/bookings.json";

        public static Application Create(string[] args)
        {
            var commandLineArgs = ParseCommandLineArguments(args);
            var (hotelsFilePath, bookingsFilePath) = GetDataFilePaths(commandLineArgs);

            var fileService = new FileService();
            var hotelRepository = new HotelRepository(fileService, hotelsFilePath);
            var bookingRepository = new BookingRepository(fileService, bookingsFilePath);
            var availabilityService = new AvailabilityService(hotelRepository, bookingRepository);

            return new Application(availabilityService);
        }

        private static Dictionary<string, string> ParseCommandLineArguments(string[] args)
        {
            if (args.Length == 0)
            {
                return new();
            }

            var options = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--hotels" || args[i] == "--bookings")
                {
                    string key = args[i];
                    if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                    {
                        string value = args[i + 1];
                        options[key] = value;
                        i++;
                    }
                    else
                    {
                        throw new ArgumentException($"Error: Missing value for {key}");
                    }
                }
                else
                {
                    throw new ArgumentException($"Error: Unexpected argument {args[i]}");
                }
            }

            return options;
        }

        /// <summary>
        /// Retrieves the file paths for the hotels and bookings data. 
        /// If no command-line arguments are provided, defaults are used. 
        /// </summary>
        private static (string hotelsFilePath, string bookingsFilePath) GetDataFilePaths(Dictionary<string, string> commandLineArgs)
        {
            string hotelsFilePath;
            string bookingsFilePath;

            if (commandLineArgs.Count == 0)
            {
                hotelsFilePath = DefaultHotelsFilePath;
                bookingsFilePath = DefaultBookingsFilePath;
            }
            else
            {
                hotelsFilePath = commandLineArgs.GetValueOrDefault("--hotels", string.Empty);
                bookingsFilePath = commandLineArgs.GetValueOrDefault("--bookings", string.Empty);
            }

            return (hotelsFilePath, bookingsFilePath);
        }
    }
}
