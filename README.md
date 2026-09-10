# Football Events API

A backend service built with ASP.NET Core designed to process football match results and compute dynamic team statistics for the Alan Systems recruitment task.

## Tech Stack & Architecture

* **ASP.NET Core (.NET 8)** - REST API routing and dependency injection.
* **Entity Framework Core & SQLite** - Data persistence maintaining chronological event ordering.
* **CQRS (MediatR)** - Clean separation of business operations via dedicated handlers.
* **Serilog** - Console logging for testing expectations and structured file logging.

## Core Features

* Processes finished match results and logs simplified team statistics.
* REST endpoints returning structured JSON data (form W/D/L, average goals, points, goal records).
* Fallback mechanism returning zero-filled data for non-existent teams.

## Getting Started & Execution

1. Ensure the **.NET 8 SDK** is installed on your machine.
2. Open a terminal in the root project directory and navigate to the `api` folder.
3. Run the application:

   ```bash
   dotnet run --project FootballEvents.API
   ```

Once running, open your browser and navigate to the Swagger UI using the local address provided in the terminal logs (typically https://localhost:5001/swagger or http://localhost:5000/swagger) to test the endpoints interactively.

## Running Tests & Verification

The project includes a comprehensive test suite covering domain logic, services, and integration scenarios to ensure compliance with all task requirements.

### Execution Commands

To run all unit and integration tests from the command line, use:
```bash
dotnet test
```