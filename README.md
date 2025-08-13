# Smart Personal Financer

A platform that helps users manage expenses, set savings goals, and receive AI-powered recommendations for smarter financial decisions.

## Tech Stack
- Python (AI/ML)
- FastAPI (Python API for AI recommendations)
- ASP.NET Core (C# Web API for expense/goal management and orchestration)

## Requirements
- Track and manage expenses
- Create and monitor savings goals
- Provide AI-powered financial recommendations
- Use Python with FastAPI for AI/ML services
- Use ASP.NET Core for the main back-end API

## Installation

Prerequisites:
- .NET SDK 8.0+
- Python 3.10+
- pip

Repository structure (recommended):
- src/
  - dotnet/SmartPersonalFinancer.Api
    - Program.cs, Controllers/, Models/, Data/, appsettings.json
  - python/recommender_service
    - main.py, requirements.txt

Environment variables:
- ASP.NET Core
  - ConnectionStrings__Default (e.g., Data Source=financer.db for SQLite or a full DB connection string)
  - RecommenderService__BaseUrl (e.g., http://localhost:8000)
- Python FastAPI
  - MODEL_PATH (optional path to a serialized model, if used)
  - SERVICE_PORT (default 8000)

1) Python AI/ML service (FastAPI)
- cd src/python/recommender_service
- python -m venv .venv
- On macOS/Linux: source .venv/bin/activate
- On Windows: .venv\Scripts\activate
- Create requirements.txt (ensure it includes):
  - fastapi
  - uvicorn[standard]
  - numpy
  - pandas
- pip install -r requirements.txt
- Run service:
  - uvicorn main:app --reload --port 8000

2) ASP.NET Core API
- cd src/dotnet/SmartPersonalFinancer.Api
- Create appsettings.json with (example):
  {
    "ConnectionStrings": { "Default": "Data Source=financer.db" },
    "RecommenderService": { "BaseUrl": "http://localhost:8000" }
  }
- dotnet restore
- If using EF Core with SQLite, add packages (example):
  - dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  - dotnet add package Microsoft.EntityFrameworkCore.Design
- (Optional) Create and apply migrations:
  - dotnet ef migrations add InitialCreate
  - dotnet ef database update
- Run API:
  - dotnet run

## Usage
- Start the Python FastAPI recommender service first (default http://localhost:8000)
- Start the ASP.NET Core API (default http://localhost:5000 or http://localhost:5187 depending on profile)

Example workflow with curl:
- Add an expense:
  curl -X POST http://localhost:5187/api/expenses -H "Content-Type: application/json" -d '{"amount": 45.0, "category": "Food", "note": "Lunch", "date": "2025-08-01"}'
- Create a savings goal:
  curl -X POST http://localhost:5187/api/goals -H "Content-Type: application/json" -d '{"name": "Emergency Fund", "targetAmount": 1000, "targetDate": "2025-12-31"}'
- Get recommendations:
  curl -X POST http://localhost:5187/api/recommendations -H "Content-Type: application/json" -d '{"months": 3}'

Swagger/OpenAPI:
- ASP.NET Core typically exposes Swagger in Development at /swagger when configured.
- FastAPI automatically serves docs at /docs and /redoc.

## Implementation Steps
1. Define architecture: use ASP.NET Core Web API as the primary back-end (expenses, goals, orchestration) and a Python FastAPI microservice for AI recommendations.
2. In ASP.NET Core, create Models: Expense { Id, Amount, Category, Note, Date }, SavingsGoal { Id, Name, TargetAmount, TargetDate, CurrentAmount(optional) }.
3. Implement Data access (e.g., EF Core with SQLite for development): DbContext with DbSets for Expenses and SavingsGoals, migrations for schema.
4. Build Controllers in ASP.NET Core: ExpensesController and GoalsController with CRUD endpoints.
5. Add RecommendationsController in ASP.NET Core that aggregates recent expenses and goals, then calls the Python FastAPI recommender via HttpClient using RecommenderService:BaseUrl.
6. Implement a typed HttpClient in ASP.NET Core and register it in Program.cs using configuration for the recommender service base URL.
7. Create the Python FastAPI service with endpoints: GET /health and POST /recommendations.
8. In the Python service, implement a simple baseline recommendation engine (e.g., analyze recent spending by category vs. goals) and return actionable suggestions; load a model from MODEL_PATH if available.
9. Add validation schemas in both services (C# DTOs and Pydantic models) to ensure consistent payloads.
10. Configure environment-based settings: appsettings.json for development with overrides via environment variables; FastAPI port via SERVICE_PORT.
11. Test end-to-end flows: create expenses/goals in ASP.NET Core, request recommendations, verify FastAPI call and response mapping.
12. Add basic logging in both services for request/response tracing and errors.

## API Endpoints

ASP.NET Core API (default base: http://localhost:5187)
- Expenses
  - GET /api/expenses
  - GET /api/expenses/{id}
  - POST /api/expenses
  - PUT /api/expenses/{id}
  - DELETE /api/expenses/{id}
- Savings Goals
  - GET /api/goals
  - GET /api/goals/{id}
  - POST /api/goals
  - PUT /api/goals/{id}
  - DELETE /api/goals/{id}
- Recommendations (orchestrates call to Python service)
  - POST /api/recommendations
    - Request example: { "months": 3 }
    - Response: { "tips": ["..."], "categoryBudgets": { "Food": 200, "Transport": 100 }, "projectedSavings": 123.45 }

Python FastAPI Recommender (default base: http://localhost:8000)
- GET /health → { "status": "ok" }
- POST /recommendations
  - Expected input: { "expenses": [ { "amount": number, "category": string, "date": string } ], "goals": [ { "name": string, "targetAmount": number, "targetDate": string } ] }
  - Returns insights such as tips, suggested category budgets, and projected savings