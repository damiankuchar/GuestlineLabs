using GuestlineLabs.App.Models;
using GuestlineLabs.App.Repositories;
using GuestlineLabs.App.Services;
using Moq;
using System.Text.Json;

namespace GuestlineLabs.UnitTests
{
    public class HotelRepositoryTests
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private const string FilePath = "hotels.json";

        public HotelRepositoryTests()
        {
            _fileServiceMock = new Mock<IFileService>();
        }

        [Fact]
        public void Constructor_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);

            // Act
            var action = () => new HotelRepository(_fileServiceMock.Object, FilePath);

            // Assert
            var exception = Assert.Throws<FileNotFoundException>(action);
        }

        [Fact]
        public void GetHotelById_ThrowsArgumentException_WhenHotelNotFound()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new() { Id = "H1", Name = "Hotel California" },
                new() { Id = "H2", Name = "Hotel Vegas" }
            };

            var serializedHotels = JsonSerializer.Serialize(hotels);

            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(fs => fs.ReadFileAllText(It.IsAny<string>())).Returns(serializedHotels);

            var hotelRepository = new HotelRepository(_fileServiceMock.Object, FilePath);

            // Act
            var action = () => hotelRepository.GetHotelById("H3");

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void GetHotelById_ReturnsCorrectHotel_WhenHotelExists()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new() { Id = "H1", Name = "Hotel California" },
                new() { Id = "H2", Name = "Hotel Vegas" }
            };

            var serializedHotels = JsonSerializer.Serialize(hotels);

            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(fs => fs.ReadFileAllText(It.IsAny<string>())).Returns(serializedHotels);

            var hotelRepository = new HotelRepository(_fileServiceMock.Object, FilePath);

            // Act
            var hotel = hotelRepository.GetHotelById("H1");

            // Assert
            Assert.NotNull(hotel);
            Assert.Equal("Hotel California", hotel.Name);
        }

        [Fact]
        public void GetRoomCount_ReturnsCorrectRoomCount_ForGivenRoomType()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new() {
                    Id = "H1",
                    Name = "Hotel California",
                    Rooms = new List<Room>
                    {
                        new() { RoomType = "SGL" },
                        new() { RoomType = "DBL" },
                        new() { RoomType = "SGL" }
                    }
                }
            };

            var serializedHotels = JsonSerializer.Serialize(hotels);

            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(fs => fs.ReadFileAllText(It.IsAny<string>())).Returns(serializedHotels);

            var hotelRepository = new HotelRepository(_fileServiceMock.Object, FilePath);

            // Act
            var roomCount = hotelRepository.GetRoomCount("H1", "SGL");

            // Assert
            Assert.Equal(2, roomCount);
        }

        [Fact]
        public void GetRoomCount_ReturnsZero_WhenRoomTypeNotFound()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new() {
                    Id = "H1",
                    Name = "Hotel California",
                    Rooms = new List<Room>
                    {
                        new() { RoomType = "SGL" },
                        new() { RoomType = "DBL" },
                        new() { RoomType = "SGL" }
                    }
                }
            };

            var serializedHotels = JsonSerializer.Serialize(hotels);

            _fileServiceMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(fs => fs.ReadFileAllText(It.IsAny<string>())).Returns(serializedHotels);

            var hotelRepository = new HotelRepository(_fileServiceMock.Object, FilePath);

            // Act
            var roomCount = hotelRepository.GetRoomCount("H1", "Suite");

            // Assert
            Assert.Equal(0, roomCount);
        }
    }
}
