
# ASP.NET Core Web Application Assignment – Project Management with Authentication and Task Tracking

## 🎯 Overall Goal

Build a web application using ASP.NET Core that allows authenticated users to:
- Register and log in using JWT.
- Create, update, retrieve, and delete **projects**.
- Manage **tasks** under each project.
- Include **unit tests** for important operations.
- Follow **Layered Architecture**, and ensure all DB operations are asynchronous using `async/await`.

---

## ✅ Features Required

### 1. Authentication System (JWT)
- **Register endpoint**: Sign up with username and password (hashed).
- **Login endpoint**: Validate credentials and return a JWT token.
- **JWT-based Authorization**: Secured endpoints for authenticated users.

### 2. Project Management
Authenticated users can:
- `POST /api/projects` – Create Project
- `GET /api/projects` – Retrieve own Projects
- `PUT /api/projects/{id}` – Update Project
- `DELETE /api/projects/{id}` – Delete Project

### 3. Task Management (Per Project)
Each project can have multiple tasks:
- `POST /api/projects/{projectId}/tasks` – Add Task
- `GET /api/projects/{projectId}/tasks` – Retrieve Tasks
- `PUT /api/tasks/{id}` – Update Task
- `DELETE /api/tasks/{id}` – Delete Task

### 4. Unit Testing (Mandatory)
- Use xUnit or NUnit.
- Test Project creation and Task creation.
- Use EF in-memory or Moq for mocking.

---

## 💻 Technical Stack

| Requirement     | Details                     |
|----------------|-----------------------------|
| Backend         | ASP.NET Core (latest)       |
| Database        | SQL Server                  |
| ORM             | Entity Framework Core       |
| Authentication  | JWT (Json Web Token)        |
| Structure       | Layered Architecture        |
| Frontend        | Optional (or Postman tests) |
| Testing         | xUnit + Moq                 |
| Async Code      | Use `async/await`           |

---

## 🧱 Suggested Project Structure

```
- ProjectManagementApp/
  - Controllers/
  - Services/
  - Repositories/
  - Data/
  - DTOs/
  - Models/
  - Tests/
```

---

## 🔐 JWT Auth Flow

1. Register → `POST /api/auth/register`
2. Login → `POST /api/auth/login` → JWT Token
3. Send `Authorization: Bearer <token>` with requests
4. Secure all endpoints

---

## ✍️ Example Use Case

> User logs in → creates project "My App" → adds tasks "Design DB", "Build API", etc.

---

## 🧪 Sample Unit Test

```csharp
[Fact]
public async Task CreateProject_ShouldAddProject_WhenValidInput()
{
    var projectService = new ProjectService(...);
    var input = new ProjectDto { Name = "Test", Description = "Testing" };

    var result = await projectService.CreateProjectAsync(userId, input);

    Assert.NotNull(result);
    Assert.Equal("Test", result.Name);
}
```

---

## ⏱️ Estimated Time

- Project setup + Auth: ~1 hour
- Project & Task CRUD: ~1 hour
- Unit Tests & cleanup: ~1 hour

---

## 🧠 Why This Test?

To assess:
- Real-world API design
- Clean code & layered architecture
- JWT & EF Core
- Async programming
- Testing

---

## 📦 Project Type

### ✅ ASP.NET Core Web API

- Template: `ASP.NET Core Web API`
- CLI Command:
  ```bash
  dotnet new webapi -n ProjectManagementApp
  ```
- Use JWT auth, EF Core with SQL Server, async/await, unit tests.
- Target Framework: `.NET 8`
  ```bash
  dotnet new webapi --framework net8.0
  ```

---

