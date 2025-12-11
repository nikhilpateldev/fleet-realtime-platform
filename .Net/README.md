# 🚚 FleetSignalR – Real-Time Fleet Operations Platform

A sample **enterprise-style** real-time fleet monitoring system built with:

- ASP.NET Core Web API
- SignalR for real-time communication
- Clean Architecture (Domain / Application / Infrastructure / API)
- EF Core (SQL Server)
- MediatR (CQRS style)
- JWT-ready authentication hooks

> This project is intentionally simplified so it can be used as a learning / GitHub portfolio repo,
> while still showing realistic patterns and a non‑trivial SignalR usage.

## Projects

- `Fleet.Domain` – Entities, value objects, enums
- `Fleet.Application` – DTOs, CQRS handlers, interfaces
- `Fleet.Infrastructure` – EF Core, repositories
- `Fleet.API` – HTTP + SignalR endpoints (main host)

## Getting Started

1. Install .NET 8 SDK.
2. Restore & build:

   ```bash
   dotnet restore
   dotnet build
   ```

3. Update `appsettings.Development.json` with a valid SQL Server connection string.
4. Run migrations (optional – or let EF create database at runtime).
5. Run the API:

   ```bash
   dotnet run --project src/Fleet.API/Fleet.API.csproj
   ```

6. Open Swagger at `https://localhost:5001/swagger` (or the URL shown in the console).

## SignalR Hubs

Mapped in `Program.cs`:

- `/hubs/tracking` – live vehicle locations
- `/hubs/trips` – trip lifecycle updates
- `/hubs/drivers` – driver online/offline & status
- `/hubs/alerts` – maintenance & system alerts

## Notes

- JWT authentication is wired in a way that you can easily plug your Identity solution.
- The project focuses on demonstrating structure + SignalR usage, not on advanced security / production hardening.
