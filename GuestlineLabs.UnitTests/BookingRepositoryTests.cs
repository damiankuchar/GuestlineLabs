using GuestlineLabs.App.Models;
using GuestlineLabs.App.Repositories;
using GuestlineLabs.App.Services;
using Moq;
using System.Text.Json;

namespace GuestlineLabs.UnitTests
{
    public class BookingRepositoryTests
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private const string FilePath = "bookings.json";

        public BookingRepositoryTests()
        {
            _fileServiceMock = new Mock<IFileService>();
        }

        [Fact]
        public void Constructor_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);

            // Act
            var action = () => new BookingRepository(_fileServiceMock.Object, FilePath);

            // Assert
            var exception = Assert.Throws<FileNotFoundException>(action);
        }

        [Fact]
        public void GetBookingCount_ReturnsCorrectCount()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new() { HotelId = "Hotel1", RoomType = "SGL", Arrival = "20241101", Departure = "20241105" },
                new() { HotelId = "Hotel1", RoomType = "SGL", Arrival = "20241103", Departure = "20241107" },
                new() { HotelId = "Hotel1", RoomType = "DBL", Arrival = "20241101", Departure = "20241105" }
            };

            var serializedBookings = JsonSerializer.Serialize(bookings);

            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(fs => fs.ReadFileAllText(It.IsAny<string>())).Returns(serializedBookings);

            var bookingRepository = new BookingRepository(_fileServiceMock.Object, FilePath);

            // Act
            var count = bookingRepository.GetBookingCount("Hotel1", "SGL", DateTime.Parse("2024-11-01"), DateTime.Parse("2024-11-06"));

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void GetBookingCount_ReturnsZero_WhenNoBookingsMatchCriteria()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new() { HotelId = "Hotel1", RoomType = "Single", Arrival = "20241101", Departure = "20241105" }
            };

            var serializedBookings = JsonSerializer.Serialize(bookings);

            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(fs => fs.ReadFileAllText(It.IsAny<string>())).Returns(serializedBookings);

            var bookingRepository = new BookingRepository(_fileServiceMock.Object, FilePath);

            // Act
            var count = bookingRepository.GetBookingCount("Hotel1", "Double", DateTime.Parse("2024-11-01"), DateTime.Parse("2024-11-05"));

            // Assert
            Assert.Equal(0, count);
        }
    }
}
