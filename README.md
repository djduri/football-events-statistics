# Football Events API

A backend service built with ASP.NET Core designed to process football match results and compute dynamic team statistics for the Alan Systems recruitment task.

## Architectural Overview & Tech Stack

* **ASP.NET Core (.NET 8):** Chosen for its high performance, robust routing, dependency injection container, and standard REST API development capabilities.
* **Entity Framework Core & SQLite:** Provides reliable data persistence required to maintain the strict chronological order of incoming football events[cite: 1]. SQLite offers a zero-configuration, file-based database ideal for automated evaluation.
* **Serilog:** Implemented for dual-sink logging, outputting minimalist raw text to the console to meet test requirements while maintaining detailed structured JSON logs on disk.
* **Domain-Driven Design (DDD):** Organizes business rules cleanly, separating domain entities, services, and database infrastructure.

## Core Functionality

* **Events Result Service Handling:** Automatically processes finished match notifications, persists match scores, updates cumulative metrics, and logs simplified statistics for participating teams[cite: 1].
* **Statistics Service REST API:** Responds to team-specific queries with structured JSON containing performance data based on the latest 3 match results, form tracking (W/D/L), average goals, matches played, points, and goal records[cite: 1].
* **Domain Validation & Resilience:** Enforces business rules and handles non-existent team queries gracefully by returning zero-filled fallback data.

## Getting Started & Execution

1. Ensure the **.NET 8 SDK** is installed on your machine.
2. Open a terminal in the root project directory and navigate to the `api` folder.
3. Run the application:
   ```bash
   dotnet run --project FootballEvents.API

## Running Tests & Verification

The project includes a comprehensive test suite covering domain logic, services, and integration scenarios to ensure compliance with all task requirements[cite: 1].

### Execution Commands

To run all unit and integration tests from the command line, use:
```bash
dotnet test

# Run domain unit tests
dotnet test FootballEvents.Domain.UnitTests

# Run integration tests
dotnet test FootballEvents.Application.IntegrationTests