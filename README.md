# OrderProcessingSystem

This is a simple **Order Processing System** API built with ASP.NET Core. It allows you to manage customer orders by creating, retrieving, and validating orders and customers through well-defined RESTful API endpoints.

## Project Setup Instructions

Follow these steps to set up the project locally:

### Prerequisites

- .NET 6.0 or later (for ASP.NET Core application)
- Visual Studio 2022 or later (or any compatible IDE like VSCode)
- Git (to clone the repository)
- A database (SQL Server, PostgreSQL, etc.) for persistent storage (if required)

### Steps to Run the Project Locally

1. **Clone the repository:**
- git clone https://github.com/sanahaju/SanaHaju_OrderProcessingSystem.git

2. **Navigate to the project directory:**
- cd OrderProcessingSystem

3. **Update the connection string:**
- Open the appsettings.json file.
- Replace the default connection string with your actual connection string for the database.
- Example for SQL Server:

"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
}
- Make sure to replace the placeholder values (your_server, your_db, your_user, your_password) with the actual details for your database.

4. **Run the project:**
- dotnet run
- The API will be available at https://localhost:5001 (or a similar URL depending on your configuration).

5. **Running Tests:**
- To run the unit tests for this project, you can use the following command: dotnet test
- This will execute both the validation tests and functional tests defined in the OrderProcessingSystem.Tests project.

#### Additional Notes for Reviewers
- Testing: The project includes both functional tests and validation tests written in xUnit, covering key scenarios for the Order Processing API.
- Logging: Basic logging is implemented using ILogger to log errors when the API encounters issues. This can be expanded based on requirements.