using GuestlineLabs.App;

namespace GuestlineLabs.E2ETests
{
    public class E2ETests : IClassFixture<FileFixture>
    {
        [Fact]
        public void Should_ReturnCorrectAvailability_WhenRoomIsAvailable_OnSpecificDate()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath, "--bookings", FileFixture.TestBookingsFilePath];
            var app = ApplicationFactory.Create(args);

            var input = new StringReader("Availability(H1, 20240901, SGL)\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            app.Run();

            // Assert
            var result = output.ToString();
            Assert.Contains("Availability for SGL in H1 from 2024-09-01 to 2024-09-01: 2", result);
        }

        [Fact]
        public void Should_ReturnCorrectAvailability_WhenRoomIsAvailable_OnSpecificDateRange()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath, "--bookings", FileFixture.TestBookingsFilePath];
            var app = ApplicationFactory.Create(args);

            var input = new StringReader("Availability(H1, 20240901-20240903, DBL)\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            app.Run();

            // Assert
            var result = output.ToString();
            Assert.Contains("Availability for DBL in H1 from 2024-09-01 to 2024-09-03: 1", result);
        }

        [Fact]
        public void Should_ReturnNegativeAvailability_WhenRoomIsOverbooked_OnSpecificDate()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath, "--bookings", FileFixture.TestBookingsFilePath];
            var app = ApplicationFactory.Create(args);

            var input = new StringReader("Availability(H1, 20240901, TRL)\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            app.Run();

            // Assert
            var result = output.ToString();
            Assert.Contains("Availability for TRL in H1 from 2024-09-01 to 2024-09-01: -1", result);
        }

        [Fact]
        public void Should_ReturnCorrectAvailability_WhenRoomIsOverbooked_OnEndOfBookingDate()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath, "--bookings", FileFixture.TestBookingsFilePath];
            var app = ApplicationFactory.Create(args);

            var input = new StringReader("Availability(H1, 20240903, TRL)\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            app.Run();

            // Assert
            var result = output.ToString();
            Assert.Contains("Availability for TRL in H1 from 2024-09-03 to 2024-09-03: 1", result);
        }

        [Fact]
        public void Should_ThrowFileNotFoundException_WhenAllFilePathsNotExists()
        {
            // Arrange
            string[] args = ["--hotels", "/path_that_does_not_exist", "--bookings", "/path_that_does_not_exist"];

            // Act
            var action = () => ApplicationFactory.Create(args);

            // Assert
            Assert.Throws<FileNotFoundException>(action);
        }

        [Fact]
        public void Should_ThrowFileNotFoundException_WhenOnlyOneFilePathExists()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath, "--bookings", "/path_that_does_not_exist"];

            // Act
            var action = () => ApplicationFactory.Create(args);

            // Assert
            Assert.Throws<FileNotFoundException>(action);
        }

        [Fact]
        public void Should_ThrowFileNotFoundException_WhenOnlyHotelsFileArgDefined()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath];

            // Act
            var action = () => ApplicationFactory.Create(args);

            // Assert
            Assert.Throws<FileNotFoundException>(action);
        }

        [Fact]
        public void Should_ThrowFileNotFoundException_WhenOnlyBookingssFileArgDefined()
        {
            // Arrange
            string[] args = ["--bookings", FileFixture.TestBookingsFilePath];

            // Act
            var action = () => ApplicationFactory.Create(args);

            // Assert
            Assert.Throws<FileNotFoundException>(action);
        }

        [Fact]
        public void Should_PrintErrorMessage_WhenBookingDateInvalidFormat()
        {
            // Arrange
            string[] args = ["--hotels", FileFixture.TestHotelsFilePath, "--bookings", FileFixture.TestBookingsFilePathInvalidDate];
            var app = ApplicationFactory.Create(args);

            var input = new StringReader("Availability(H1, 20240903, TRL)\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            app.Run();

            // Assert
            var result = output.ToString();
            Assert.Contains("was not recognized as a valid DateTime", result);
        }
    }
}
