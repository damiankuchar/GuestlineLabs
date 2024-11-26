namespace GuestlineLabs.E2ETests
{
    public class FileFixture : IDisposable
    {
        public const string TestHotelsFilePath = "test_hotels.json";
        public const string TestBookingsFilePath = "test_bookings.json";
        public const string TestBookingsFilePathInvalidDate = "invalid_date_test_bookings.json";

        public FileFixture()
        {
            File.WriteAllText(TestHotelsFilePath, GetSampleHotelsJson());
            File.WriteAllText(TestBookingsFilePath, GetSampleBookingsJson());
            File.WriteAllText(TestBookingsFilePathInvalidDate, GetSampleBookingsJsonInvalidDate());
        }

        public void Dispose()
        {
            if (File.Exists(TestHotelsFilePath))
            {
                File.Delete(TestHotelsFilePath);
            }

            if (File.Exists(TestBookingsFilePath))
            {
                File.Delete(TestBookingsFilePath);
            }

            if (File.Exists(TestBookingsFilePathInvalidDate))
            {
                File.Delete(TestBookingsFilePathInvalidDate);
            }
        }

        private string GetSampleHotelsJson()
        {
            return @"
            [
              {
                ""id"": ""H1"",
                ""name"": ""Hotel California"",
                ""roomTypes"": [
                  {
                    ""code"": ""SGL"",
                    ""description"": ""Single Room"",
                    ""amenities"": [
                      ""WiFi"",
                      ""TV""
                    ],
                    ""features"": [
                      ""Non-smoking""
                    ]
                  },
                  {
                    ""code"": ""DBL"",
                    ""description"": ""Double Room"",
                    ""amenities"": [
                      ""WiFi"",
                      ""TV"",
                      ""Minibar""
                    ],
                    ""features"": [
                      ""Non-smoking"",
                      ""Sea View""
                    ]
                  },
{
                    ""code"": ""TRL"",
                    ""description"": ""Triple Room"",
                    ""amenities"": [
                      ""WiFi"",
                      ""TV"",
                      ""Minibar""
                    ],
                    ""features"": [
                      ""Non-smoking"",
                      ""Sea View""
                    ]
                  }
                ],
                ""rooms"": [
                  {
                    ""roomType"": ""SGL"",
                    ""roomId"": ""101""
                  },
                  {
                    ""roomType"": ""SGL"",
                    ""roomId"": ""102""
                  },
                  {
                    ""roomType"": ""DBL"",
                    ""roomId"": ""201""
                  },
                  {
                    ""roomType"": ""DBL"",
                    ""roomId"": ""202""
                  },
                  {
                    ""roomType"": ""TRL"",
                    ""roomId"": ""301""
                  }
                ]
              }
            ]
            ";
        }

        private string GetSampleBookingsJson()
        {
            return @"
            [
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""20240901"",
                ""departure"": ""20240903"",
                ""roomType"": ""DBL"",
                ""roomRate"": ""Prepaid""
              },
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""20240902"",
                ""departure"": ""20240905"",
                ""roomType"": ""SGL"",
                ""roomRate"": ""Standard""
              },
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""20240901"",
                ""departure"": ""20240903"",
                ""roomType"": ""TRL"",
                ""roomRate"": ""Prepaid""
              },
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""20240901"",
                ""departure"": ""20240903"",
                ""roomType"": ""TRL"",
                ""roomRate"": ""Prepaid""
              }
            ]
            ";
        }

        private string GetSampleBookingsJsonInvalidDate()
        {
            return @"
            [
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""2024-09-01"",
                ""departure"": ""2024-09-03"",
                ""roomType"": ""DBL"",
                ""roomRate"": ""Prepaid""
              },
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""2024-09-02"",
                ""departure"": ""2024-09-05"",
                ""roomType"": ""SGL"",
                ""roomRate"": ""Standard""
              },
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""2024-09-01"",
                ""departure"": ""2024-09-03"",
                ""roomType"": ""TRL"",
                ""roomRate"": ""Prepaid""
              },
              {
                ""hotelId"": ""H1"",
                ""arrival"": ""2024-09-01"",
                ""departure"": ""2024-09-03"",
                ""roomType"": ""TRL"",
                ""roomRate"": ""Prepaid""
              }
            ]
            ";
        }
    }
}
