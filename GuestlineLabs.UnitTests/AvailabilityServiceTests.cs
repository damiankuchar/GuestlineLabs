using GuestlineLabs.App.Queries;
using GuestlineLabs.App.Repositories;
using GuestlineLabs.App.Services;
using Moq;

namespace GuestlineLabs.UnitTests
{
    public class AvailabilityServiceTests
    {
        private readonly Mock<IHotelRepository> _mockHotelRepository;
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly AvailabilityService _availabilityService;

        public AvailabilityServiceTests()
        {
            _mockHotelRepository = new Mock<IHotelRepository>();
            _mockBookingRepository = new Mock<IBookingRepository>();
            _availabilityService = new AvailabilityService(_mockHotelRepository.Object, _mockBookingRepository.Object);
        }

        [Fact]
        public void CheckAvailability_ShouldReturnAvailableRooms_WhenRoomsAreAvailable()
        {
            // Arrange
            var query = AvailabilityQuery.Parse("Availability(H1, 20240901, SGL)");

            _mockHotelRepository.Setup(repo => repo.GetRoomCount(query.HotelId, query.RoomType)).Returns(10);
            _mockBookingRepository.Setup(repo => repo.GetBookingCount(query.HotelId, query.RoomType, query.StartDate, query.EndDate)).Returns(3);

            // Act
            var result = _availabilityService.CheckAvailability(query);

            // Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void CheckAvailability_ShouldReturnZero_WhenNoRoomsAreAvailable()
        {
            // Arrange
            var query = AvailabilityQuery.Parse("Availability(H1, 20240901, SGL)");

            _mockHotelRepository.Setup(repo => repo.GetRoomCount(query.HotelId, query.RoomType)).Returns(5);
            _mockBookingRepository.Setup(repo => repo.GetBookingCount(query.HotelId, query.RoomType, query.StartDate, query.EndDate)).Returns(5);

            // Act
            var result = _availabilityService.CheckAvailability(query);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CheckAvailability_ShouldReturnAllRooms_WhenNoBookingsExist()
        {
            // Arrange
            var query = AvailabilityQuery.Parse("Availability(H1, 20240901, SGL)");

            _mockHotelRepository.Setup(repo => repo.GetRoomCount(query.HotelId, query.RoomType)).Returns(5);
            _mockBookingRepository.Setup(repo => repo.GetBookingCount(query.HotelId, query.RoomType, query.StartDate, query.EndDate)).Returns(0);

            // Act
            var result = _availabilityService.CheckAvailability(query);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void CheckAvailability_ShouldHandleEdgeCase_WhenQueryHasNoRooms()
        {
            // Arrange
            var query = AvailabilityQuery.Parse("Availability(H1, 20240901, SGL)");

            _mockHotelRepository.Setup(repo => repo.GetRoomCount(query.HotelId, query.RoomType)).Returns(0);
            _mockBookingRepository.Setup(repo => repo.GetBookingCount(query.HotelId, query.RoomType, query.StartDate, query.EndDate)).Returns(0);

            // Act
            var result = _availabilityService.CheckAvailability(query);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CheckAvailability_ShouldReturnNegative_WhenRoomsAreOverbooked()
        {
            // Arrange
            var query = AvailabilityQuery.Parse("Availability(H1, 20240901, SGL)");

            _mockHotelRepository.Setup(repo => repo.GetRoomCount(query.HotelId, query.RoomType)).Returns(5);
            _mockBookingRepository.Setup(repo => repo.GetBookingCount(query.HotelId, query.RoomType, query.StartDate, query.EndDate)).Returns(10);

            // Act
            var result = _availabilityService.CheckAvailability(query);

            // Assert
            Assert.Equal(-5, result);
        }
    }
}
