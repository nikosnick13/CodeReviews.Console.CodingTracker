# Coding Tracker Application

This is a console-based application developed in C# that helps users track their coding sessions. The Coding Tracker Application enables users to log the start and end times of their coding sessions, view past records, update entries, and delete records. This project uses SQLite for storing session data in a local database, making it lightweight and easy to use.

## Features

- **Add Coding Session: Records a coding session's start and end times** 
- **View All Records: Displays all stored coding sessions in a table format, showing session durations and times.**

- **Update Session: Allows users to update existing coding session start and end times.**
- **Delete Session: Enables users to remove specific coding records from the database.**
- **Session Duration Calculation: Automatically calculates and displays the duration of each coding session.**

## Prerequisites
Before running this application, make sure you have the following installed:
- [.NET SDK 5.0 or later](https://dotnet.microsoft.com/download)
- SQLite

## Dependencies
The following NuGet packages are required for this project:
- [Spectre.Console](https://www.nuget.org/packages/Spectre.Console/0.49.1) — Provides rich console output for user interaction.
- [Dapper](https://www.nuget.org/packages/Dapper/2.1.35) — A lightweight ORM for data access.
- [System.Configuration.ConfigurationManager](https://www.nuget.org/packages/System.Configuration.ConfigurationManager/8.0.1) — Manages configuration settings.
- [ConsoleTableExt](https://www.nuget.org/packages/ConsoleTableExt/3.3.0) — Used for displaying data in table format in the console.
- [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite/8.0.10) — Allows interaction with SQLite databases.

## Getting Started
1. Clone the repository and navigate to the project folder.
2. Install the dependencies using the NuGet package manager or by adding them directly in your project file:
    ```bash
    dotnet add package Spectre.Console --version 0.49.1
    dotnet add package Dapper --version 2.1.35
    dotnet add package System.Configuration.ConfigurationManager --version 8.0.1
    dotnet add package ConsoleTableExt --version 3.3.0
    dotnet add package Microsoft.Data.Sqlite --version 8.0.10
    ```
3. Build and run the application:
    ```bash
    dotnet run
    ```

## Usage
Once the application is running, you can choose to:
- **Add**: Record a new coding session by specifying start and end times.
- **View**: View all recorded sessions.
- **Edit**: Update an existing session by ID.
- **Delete**: Remove a session from records.

## License
This project is licensed under the MIT License.

---