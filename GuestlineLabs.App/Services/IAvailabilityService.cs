using GuestlineLabs.App.Queries;

namespace GuestlineLabs.App.Services
{
    public interface IAvailabilityService
    {
        int CheckAvailability(AvailabilityQuery query);
    }
}
