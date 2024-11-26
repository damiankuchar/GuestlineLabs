using System.Text.Json;

namespace GuestlineLabs.App.Services
{
    public class JsonOptionsService
    {
        public static readonly JsonSerializerOptions Default = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
