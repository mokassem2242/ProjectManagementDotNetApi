# ProjectManagementAppTask

## Overview

ProjectManagementAppTask is an ASP.NET Core Web API designed for project management with authentication and task tracking. It allows users to register, log in (using JWT), create and manage projects, and manage tasks within those projects. The application follows a layered architecture and uses Entity Framework Core for data access, supporting asynchronous operations throughout.

## Architectural Patterns and Practices

### Clean Architecture
We use **Clean Architecture** to ensure a clear separation of concerns within the application. The solution is divided into distinct layers:
- **API Layer:** Handles HTTP requests and responses.
- **Application Layer:** Contains business logic, use cases, and service abstractions.
- **Domain Layer:** Defines core entities and domain logic.
- **Infrastructure Layer:** Implements data access, authentication, and external integrations.

This structure makes the codebase maintainable, testable, and scalable.

### CQRS (Command Query Responsibility Segregation)
The application follows the **CQRS** pattern, separating read (query) and write (command) operations. Each operation is represented by a specific command or query class, making the codebase more organized and easier to extend. This also improves scalability and clarity, as commands mutate state and queries only retrieve data.

### MediatR
We use **MediatR** as a mediator library to decouple the sending of commands/queries from their handling. Controllers send commands and queries to MediatR, which then dispatches them to the appropriate handler. This reduces direct dependencies and keeps controllers thin.

### FluentValidation
**FluentValidation** is used for validating incoming requests. Each command or query can have an associated validator class, ensuring that only valid data reaches the business logic. This approach centralizes validation logic and provides clear, maintainable rules.

### Global Exception Handling
A custom **global exception handling middleware** is implemented to catch and process unhandled exceptions across the API. This ensures that errors are logged and consistent error responses are returned to clients, improving reliability and user experience.

### Global Response Wrapping
All API responses are wrapped in a **standardized response model**. This provides a consistent structure for success and error responses, making it easier for clients to consume the API and handle results uniformly.

## Features
- **User Authentication:** Register and log in with JWT-based authentication.
- **Project Management:** Create, retrieve, update, and delete projects.
- **Task Management:** Add, retrieve, update, and delete tasks within projects.
- **Layered Architecture:** Clean separation of concerns across API, Application, Domain, and Infrastructure layers.
- **Asynchronous Operations:** All database operations use async/await for scalability.
- **Unit Testing:** Includes unit tests for core features using xUnit and Moq.

## Project Structure

```
ProjectManagementAppTask/
│
├── planning/                         # Assignment and planning documentation
│   └── ProjectAssignment_AspNetCore.md
│
├── ProjectManagementApp.Api/         # API layer (controllers, middleware, startup)
│   ├── Controllers/                  # API endpoints for Auth, Projects, Tasks
│   ├── Middleware/                   # Custom middleware (e.g., exception handling)
│   ├── Program.cs                    # Application entry point
│   └── ...
│
├── ProjectManagementApp.Application/ # Application layer (business logic)
│   ├── Features/                     # CQRS commands and queries for Projects/Tasks
│   ├── Contracts/                    # Interfaces for services and repositories
│   ├── Models/                       # DTOs and request/response models
│   └── ...
│
├── ProjectManagementApp.Domain/      # Domain layer (core entities and logic)
│   ├── Entities/                     # MainProject, ProjectTask, DomainUser, etc.
│   └── Common/                       # Base classes (e.g., AuditableEntity)
│
├── ProjectManagementApp.Infrastructure/ # Infrastructure layer (data, identity)
│   ├── Persistence/                  # EF Core DbContext and repositories
│   ├── Identity/                     # Authentication and user management
│   └── ...
│
└── README.md                         # Project documentation (this file)
```

## Technical Stack
- **Backend:** ASP.NET Core Web API
- **Database:** SQL Server (via Entity Framework Core)
- **Authentication:** JWT (Json Web Token)
- **Testing:** xUnit, Moq
- **Architecture:** Layered (API, Application, Domain, Infrastructure)

## Getting Started
1. Clone the repository.
2. Configure your database connection in `appsettings.json`.
3. Run database migrations if needed.
4. Build and run the API project.
5. Use Postman or similar tools to interact with the API endpoints.

## Example Use Case
1. Register a new user via `/api/auth/register`.
2. Log in via `/api/auth/login` to receive a JWT token.
3. Use the token to create a project and add tasks to it.

## License
This project is for educational and demonstration purposes. 
