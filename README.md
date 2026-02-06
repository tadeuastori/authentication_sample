
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

> **Note on database choice (ARM64 compatibility)**  
>  
> The project uses **Azure SQL Edge** as the database engine when running locally via Docker.  
>  
> This choice was made due to **ARM64 architecture limitations on the local development machine**, as the standard SQL Server Docker image does not fully support ARM64 environments.  
>  
> Azure SQL Edge is **fully compatible with SQL Server features used in this project**, and the application can run without changes on standard SQL Server installations in non-ARM environments.


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
/README.md
/DEPLOYMENT.md
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
Server=localhost,1433;Database=AccountsDb;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True
```

### Define Password Policy

Update the PasswordPolicy in:

```
src/WebApp/appsettings.json
```

Example:

```
"PasswordPolicy": {
  "MinLength": 8,
  "MinSpecialChars": 2,
  "SpecialChars": [ "@", "#", "!", "%", "&", "*" ]
}
```

### Run Application

> ⚠️  **HTTPS configuration**  
>  
> The application is configured to support both HTTP and HTTPS endpoints.    
> However, all pages are **served only over HTTPS**, and HTTP requests are automatically redirected to HTTPS.    
> For local development, make sure you have a trusted development certificate by running:
>  
> ```bash
> dotnet dev-certs https --trust
> ```
>  
> After trusting the certificate, restart the application and access it using:
>  
> ```
> https://localhost:7175/login
> ```

Using Visual Studio:

> If you are opening the application using Visual Studio, just press F5

Using prompt, Execute: 

```
dotnet restore
dotnet build
dotnet run --project TRSB.Web
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

---

## 🧠 Use of AI-Assisted Tools

During the development of this project, AI-assisted tools were used as a **productivity and support aid**, similarly to how documentation, search engines, and code examples are commonly used in professional environments.

AI assistance was applied in the following areas:

- **Unit Tests**
  - Support in generating initial unit test structures and identifying relevant test scenarios.
  - All tests were reviewed, adapted, and validated to align with the project’s architecture and business rules.

- **Frontend (Razor Pages)**
  - Assistance with Razor syntax, page lifecycle behavior, and best practices for form handling and validation.
  - Final implementation decisions and adjustments were made manually to fit the project requirements.

- **Documentation Standardization**
  - Help with formatting, structuring, and standardizing README and deployment documentation.
  - Technical content, architecture decisions, and implementation details were defined by me.

- **Design Patterns (Strategy Pattern)**
  - Support in understanding and applying the Strategy pattern in a clean and maintainable way.
  - The final design reflects deliberate architectural choices aligned with the project constraints and simplicity requirements.

The use of AI tools was **intentional and controlled**, focusing on improving clarity, consistency, and development efficiency, while all architectural decisions, business logic, and final code remain the responsibility of me.
