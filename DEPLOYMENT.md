# Deployment & Startup Documentation (Basic Level)

---

### Overview

This document explains how to run, publish, and deploy the application on IIS, at a basic level, so that another developer can easily start, publish, and host the application.

The application is an __ASP.NET Core Web App (Razor Pages)__ built with:
- .NET 8
- Cookie-based authentication
- SQL Server (via Docker – Azure SQL Edge)

### Prerequisites
Make sure the following tools are installed:

- .NET SDK 8.0
- Docker Desktop (with Docker Compose enabled)
- Git
- Windows IIS
- ASP.NET Core Hosting Bundle (.NET 8) installed on the IIS server

---

## 1. Clone the Repository

```
git clone https://github.com/tadeuastori/authentication_sample.git
```
---

## 2. Database Setup (Docker)

> #### NOTE 1
> The choice of Azure SQL Edge was driven by local ARM64 environment constraints. This solution supports ARM64 natively and remains compatible with other architectures, including x64.

> #### NOTE 2
> Make sure the Database password are the same between the docker-compose.yml file and the ConnectionString

#### Location

The file `docker-compose.yml` is located at the root of the repository.

#### Start the database
```
docker compose up -d
```
#### Connection String

The application uses the following configuration in `appsettings.json`:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=TRSB.Accounts;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
}
```

---

## 3. Database Initialization and Migrations

The application uses **Entity Framework Core migrations** to manage the database schema.

When the application starts, it automatically checks for pending migrations and applies them to the database.  
This ensures that the database schema is created and updated without requiring manual intervention.

### Behavior on Startup

- If the database does not exist, it will be **created automatically**
- If migrations are pending, they will be **applied automatically**
- No manual SQL scripts are required to initialize the database

This behavior allows a new developer or deployment environment to start the application immediately after configuring the connection string and database container.

> ⚠️ **Important**  
> The database container must be running before starting the application, as migrations are applied during application startup.

---

## 4. Run the Application Locally

```
dotnet restore
dotnet build
dotnet run --project TRSB.Web
```

The application will be available at:

```
https://localhost:<port>/login
```

#### HTTPS Requirement

- The application accepts HTTP
- The application only serves pages over HTTPS
- HTTP requests are automatically rejected or redirected

---

## 5. Publish the Application

From the solution root:
```
dotnet publish TRSB.Web -c Release -o ./publish
```

This generates a publish-ready folder for IIS deployment.

--- 

## 6. Deploy to IIS
#### IIS Configuration Steps

1. Copy the publish folder to the server
2.  Open IIS Manager
3.  Create a new Web Site or Application
4.  Configure:
    1. Physical Path → publish folder
    2. Binding → HTTPS
5. Set the Application Pool:
    1. .NET CLR Version → No Managed Code
6. Ensure the ASP.NET Core Hosting Bundle is installed

#### HTTPS Configuration
The application uses ASP.NET Core HTTPS redirection middleware.

For HTTPS to work correctly, the hosting environment must expose an HTTPS endpoint:

- HTTPS configuration for local development is documented in the README.
- In deployed environments, HTTPS is handled by the hosting infrastructure.
- In IIS, the site must have an HTTPS binding with a valid certificate.

If HTTPS is not available, the application will run in HTTP mode only.

#### IIS and Production Considerations

When deploying to IIS:

- Ensure the **ASP.NET Core Hosting Bundle** is installed on the server
- Configure a valid **HTTPS certificate**
- Bind the application to HTTPS in IIS
- The application supports HTTP, but all pages are served over HTTPS

No additional configuration is required for database migrations, as they are executed automatically on application startup.

---

## 7. Authentication Notes

- Authentication uses Cookie Authentication
- Protected pages use the [Authorize] attribute
- Unauthenticated users are redirected to /login

---

## 8. Common Issues

Application does not start on IIS
- Verify ASP.NET Core Hosting Bundle installation

Database connection errors
- Ensure Docker container is running
- Check SQL credentials and port mapping

Login redirects back to login
- Verify HTTPS binding
- Ensure cookies are enabled

---

## Conclusion

This documentation is intentionally kept at a basic level, as requested, to allow:

- Another developer to run the application
- A separate team to publish it
- Deployment on IIS without deep project knowledge