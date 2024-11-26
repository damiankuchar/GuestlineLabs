using GuestlineLabs.App;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var app = ApplicationFactory.Create(args);
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Availability(H1, 20240901, SGL)
// Availability(H1, 20240901-20240903, DBL)