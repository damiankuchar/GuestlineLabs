namespace GuestlineLabs.App.Models
{
    public class RoomType
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Amenities { get; set; } = [];
        public List<string> Features { get; set; } = [];
    }
}
