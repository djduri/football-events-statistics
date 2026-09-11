# Football Events API

Backend service for processing football match results and calculating team statistics.

## Tech Stack
* **Core:** .NET 8 (ASP.NET Core Web API), CQRS (MediatR), FluentValidation
* **Data:** Entity Framework Core, SQLite
* **Logging:** Serilog
* **Testing:** xUnit, FluentAssertions

## Getting Started

1. Ensure the **.NET 8 SDK** is installed on your machine.
2. Navigate to the `api` folder.
3. Run the application:
   ```bash
   dotnet run --project FootballEvents.API
   ```
4. Open the Swagger UI (usually http://localhost:5000/swagger or https://localhost:5001/swagger) to test the endpoints.

## Testing

To run the unit and integration tests:
```bash
dotnet test
```

## API Endpoints

### 1. Process Single Match Result
**`POST /api/cms/Events/Result`**

Processes a match result and updates statistics.

**Request**
```json
{
  "home_team": "Bayern",
  "away_team": "Real",
  "home_score": 1,
  "away_score": 0
}
```

**Response (text/plain):**
```json
Bayern 5 10 10 5 Real 2 3 1 1
```

### 2. Process Matches from File
**`POST /api/cms/Events/ProcessFile`**

Batch processing of match results via a text file upload.

**Request: multipart/form-data with file (e.g., sample_messages.txt)**

**Example file content:**
```json
{ "home_team": "Bayern", "away_team": "Barcelona", "home_score": 3, "away_score": 0  }
{ "home_team": "Real", "away_team": "Milan", "home_score": 2, "away_score": 2  }
{ "home_team": "Milan", "away_team": "Bayern", "home_score": 1, "away_score": 2  }
{ "home_team": "Barcelona", "away_team": "Real", "home_score": 3, "away_score": 3  }
{ "home_team": "Bayern", "away_team": "Real", "home_score": 2, "away_score": 4  }
{ "teams": ["Bayern", "Milan"] } 
{ "home_team": "Milan", "away_team": "Barcelona", "home_score": 1, "away_score": 4 }
{ "home_team": "Barcelona", "away_team": "Milan", "home_score": 2, "away_score": 1 }
{ "home_team": "Real", "away_team": "Bayern", "home_score": 2, "away_score": 2 }
{ "home_team": "Real", "away_team": "Barcelona", "home_score": 6, "away_score": 1 }
{ "home_team": "Bayern", "away_team": "Milan", "home_score": 3, "away_score": 1 }
{ "home_team": "Milan", "away_team": "Real", "home_score": 2, "away_score": 3 }
{ "home_team": "Barcelona", "away_team": "Bayern", "home_score": 1, "away_score": 1 }
{ "teams": ["Bayern", "Milan", "Real", "Barcelona"] }
```

**Response (200 OK): Returns the total number of successfully processed matches.:**
```text
14
```

### 3. Get Team Statistics
**`POST /api/cms/Statistics/GetTeamStatistics`**

Returns rolling statistics (form, avg goals, points) for requested teams over their last 3 matches.

**Request:**
```json
{
  "teams": [
    "Bayern",
    "Milan"
  ]
}
```

**Response (application/json):**
```json
[
  {
    "name": "Bayern",
    "form": "DWD",
    "averageGoals": 3.33,
    "matchesPlayed": 3,
    "points": 5,
    "goalsScored": 6,
    "goalsConceded": 4
  },
  {
    "name": "Milan",
    "form": "LLL",
    "averageGoals": 4,
    "matchesPlayed": 3,
    "points": 0,
    "goalsScored": 4,
    "goalsConceded": 8
  }
]
```

## Author
Prepared by **Łukasz Kowol** for the Alan Systems recruitment process.