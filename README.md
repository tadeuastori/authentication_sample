
# ASP.NET Core Authentication Sample (Razor Pages)

This project is a simple authentication-based web application built with **ASP.NET Core Razor Pages**, created as part of a technical evaluation.  
It demonstrates **clean architecture**, **cookie-based authentication**, **basic security practices**, and **unit testing**.

---

## 🚀 Features

- User Registration  
- Login & Logout (Cookie Authentication)  
- Protected Profile page  
- HTTPS enforced (HTTP accepted but not served)  
- Clean separation of concerns  
- Basic unit tests  
- Dockerized database support  

---

## 🧱 Tech Stack

- .NET 8  
- ASP.NET Core Razor Pages  
- Cookie Authentication  
- Entity Framework Core  
- SQL Server (Docker)  
- xUnit  


> #### Note on Database Choice (ARM64 compatibility)
> Due to ARM64 architecture limitations on the development machine, the project uses mcr.microsoft.com/azure-sql-edge instead of the standard SQL Server image.
> Azure SQL Edge is fully compatible with SQL Server features required by this project and is commonly used for local development and edge scenarios on ARM-based systems.


---

## 📂 Project Structure

```
/src
  ├── WebApp
  ├── Application
  ├── Domain
  ├── Infrastructure
/tests
  └── WebApp.Tests
/docker
  └── docker-compose.yml
```

---

## 🔐 Security Notes

- HTTPS redirection is enabled
- Cookies are configured with:
  - `HttpOnly = true`
  - `SecurePolicy = Always`
- Passwords are stored using hashing (no plain text)
- Unauthorized users are automatically redirected to `/Login`

---

## ▶️ How to Run the Application

### Prerequisites

- .NET 8 SDK  
- Docker & Docker Compose  
- Git  

### Clone

```
git clone https://github.com/tadeuastori/authentication_sample.git
```

### Start the Database (Docker)

The database is provided via Docker.

📍 Location of docker-compose file:
```
/docker/docker-compose.yml
```

Run:

```
docker compose up -d
```

This will start:

- Donwload the image (azure-sql-edge)
- Start SQL Server
- Database exposed on the configured port

### Configure Connection String

Update the connection string in:

```
src/WebApp/appsettings.json
```

Example:

```
Server=localhost,1433;Database=AuthDb;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True
```

### Run Application

```
cd src/WebApp
dotnet run
```

The application will be available at:

```
https://localhost:7175/Login
```

⚠️ Accessing the HTTP version will not serve content, only HTTPS is allowed.

---

## 🧪 Tests

Simplified Tests are included for:

- Service logic validation
- Fake repositories for isolation
- Basic authentication scenarios

```
dotnet test
```

## 📌 Notes

This project was created for technical evaluation purposes and focuses on clarity, structure, and security best practices.

👤 Default Usage Flow

- Open /Login
- Register a new user
- Login with your credentials
- Access the protected /Profile page
- Logout to clear the authentication cookie

📄 Notes for Reviewers

- This project focuses on clarity, structure, and correctness
- Tests were added as a quality improvement, not as a requirement
- The architecture is intentionally simple and easy to review
- No external identity provider is used