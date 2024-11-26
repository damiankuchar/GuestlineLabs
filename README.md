# Instructions on how to build and run

## Prerequisites

Ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- [Visual Studio 2022]([https://code.visualstudio.com/](https://visualstudio.microsoft.com/pl/vs/community/)) (optional)
- [Git](https://git-scm.com/)

## Clone the Repository 🖥️
```
git clone https://github.com/damiankuchar/GuestlineLabs.git
```

## Build and run the Application 🚀
To build and run application, run the following commands in your terminal. 
```
cd GuestlineLabs\GuestlineLabs.App
dotnet run
```

or from solution level:
```
cd GuestlineLabs
dotnet run --project GuestlineLabs.App
```

Example - console input:
```
Availability(H1, 20240901, SGL)
Availability(H1, 20240901-20240903, DBL)
```

Output (for default files):
```
Enter queries (empty line to exit):
Availability(H1, 20240901, SGL)
Availability for SGL in H1 from 2024-09-01 to 2024-09-01: 2

Availability(H1, 20240901-20240903, DBL)
Availability for DBL in H1 from 2024-09-01 to 2024-09-03: 1
```

## Command-Line Arguments 🎯
By default, the application will use **default files** for easier testing if no command-line arguments are provided.

However, you can specify custom file paths for the hotels and bookings data using command-line arguments like this (the order doesn't matter):
```
dotnet run --hotels "<path-to-hotels-file>" --bookings "<path-to-bookings-file>"
```

## Running Tests 🧪
To run the tests for the application, you can use the .NET CLI `dotnet test` command.
```
cd GuestlineLabs
dotnet test
```

## Run, debug and test using Visual Studio 2022 🐞
Open solution with VS 2022

![image](https://github.com/user-attachments/assets/56b7bc49-8f4a-46fe-8131-dc0d05c15085)

Build and Run using button

![image](https://github.com/user-attachments/assets/ed1b42fa-6e8b-4446-abb2-6a36e3bdd639)

Run all tests using `Test Explorer` window

![image](https://github.com/user-attachments/assets/c48d6150-b9ef-4aa0-8094-51a4329e2abc)

