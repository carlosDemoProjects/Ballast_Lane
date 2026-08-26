# Ballast_Lane
.NET - Technical Interview Exercise 

ComicTracker
A full-stack web application for managing your personal comic and manga collection. Built with Clean Architecture, TDD, and JWT authentication.
Tech Stack
Backend

.NET 9 / ASP.NET Core Web API
Entity Framework Core 9 (SQL Server)
Clean Architecture
JWT Authentication (BCrypt password hashing)
xUnit + Moq + FluentAssertions (TDD)

Frontend
Angular 20 
Angular Material
Signals for state management

Architecture
ComicTracker/
├── src/
│   ├── ComicTracker.Domain/          # Entities, interfaces, domain exceptions
│   ├── ComicTracker.Application/     # DTOs, service interfaces, service implementations
│   ├── ComicTracker.Infrastructure/  # EF Core, repositories, JWT, Auth services
│   └── ComicTracker.API/             # Controllers, Program.cs, middleware
├── tests/
│   ├── ComicTracker.Domain.Tests/
│   └── ComicTracker.Application.Tests/
└── frontend/

    └── comic-tracker-web/            # Angular application
Layer responsibilities
Domain — Core entities (Comic, User) with factory methods and domain validations. No external dependencies.
Application — Business logic services and DTOs. Depends only on Domain interfaces.
Infrastructure — EF Core implementations, SQL Server, BCrypt, JWT token generation.
API — HTTP controllers, dependency injection wiring, middleware configuration.


Prerequisites
.NET 9 SDK
SQL Server (local instance)
Node.js 18+ and npm
Angular CLI (npm install -g @angular/cli)


Getting Started
1. Clone the repository
git clone https://github.com/your-username/ComicTracker.git

cd ComicTracker
2. Configure the database connection
Edit src/ComicTracker.API/appsettings.json:

{

  "ConnectionStrings": {

    "DefaultConnection": "Server=localhost;Database=ComicTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;"

  },

  "Jwt": {

    "Key": "ComicTracker$SuperSecretKey2024!XyZ987@Secure#Pass",

    "Issuer": "ComicTrackerAPI",

    "Audience": "ComicTrackerClient"

  }

}
3. Run database migrations
dotnet ef database update --project src/ComicTracker.Infrastructure --startup-project src/ComicTracker.API

The database is seeded automatically on first run with a demo user and sample comics.
4. Run the API
cd src/ComicTracker.API

dotnet run

API available at: https://localhost:5202
API docs (Scalar): https://localhost:5202/scalar/v1
5. Run the frontend
cd frontend/comic-tracker-web

npm install

ng serve

Frontend available at: http://localhost:4200

Demo Credentials
Field
Value
Email
demo@comictracker.com
Password
Demo1234!

API Endpoints
Auth
Method
Endpoint
Auth
Description
POST
/api/auth/register
Public
Register new user
POST
/api/auth/login
Public
Login, returns JWT
GET
/api/auth/me
Required
Current user info

Comics
Method
Endpoint
Auth
Description
GET
/api/comics
Required
Get user's comics
GET
/api/comics/{id}
Required
Get comic by ID
POST
/api/comics
Required
Add comic to collection
PUT
/api/comics/{id}
Required
Update comic
DELETE
/api/comics/{id}
Required
Remove from collection


Comics are scoped per user — each user only sees and manages their own collection.

Running Tests
dotnet test
Tests cover:
Domain layer — Entity creation, validation rules, domain exceptions
Application layer — Service logic, ownership checks, repository interactions (mocked)


Key Design Decisions
Clean Architecture — Dependencies point inward. Domain has zero external dependencies; infrastructure details are behind interfaces.
TDD — Tests were written before implementation (Red → Green → Refactor). Domain tests first, then Application tests with mocked repositories.
Per-user comic collection — Comics include a UserId foreign key. The service layer enforces ownership on every read, update, and delete operation.
JWT Bearer tokens — Tokens are validated on every protected endpoint. The Angular interceptor attaches the token automatically to outgoing requests and redirects to login on 401.
Factory methods over constructors — Comic.Create() and User.Create() enforce domain invariants at creation time with private setters preventing external mutation.
Scalar over Swashbuckle — Swashbuckle has compatibility issues with .NET 9. Using Microsoft.AspNetCore.OpenApi + Scalar.AspNetCore instead.

