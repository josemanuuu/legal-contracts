# Legal Contracts Backend API

A .NET Core 8 Web API for managing legal contracts with Entity Framework Core and PostgreSQL database support.

## Project Structure

```text
legal-contracts-back/
├── Controllers/
│   └── LegalContractsController.cs    # API endpoints for legal contracts
├── Data/
│   └── LegalContractsDbContext.cs     # Database context configuration
├── Migrations/                        # Database migration files
├── Models/
│   └── LegalContract.cs               # Data model and DTOs
├── Properties/
├── appsettings.json                   # Application configuration
├── Program.cs                         # Application entry point
├── legal-contracts-back.csproj        # Project file
└── Dockerfile                         # Containerization configuration
```

## Features

- RESTful API for CRUD operations on legal contracts
- Entity Framework Core with PostgreSQL support
- Automatic database migrations
- UTC datetime handling
- Docker containerization support
- Swagger/OpenAPI documentation in development

## Getting Started

### Prerequisites

- .NET 8 SDK
- PostgreSQL database
- Docker (optional)

### Installation

1. Clone the repository
2. Configure your database connection in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=legal_contracts;Username=postgres;Password=your_password"
  }
}
```

3. Apply database migrations:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/LegalContracts` | Get all contracts with optional date filtering |
| GET | `/api/LegalContracts/{id}` | Get a specific contract by ID |
| POST | `/api/LegalContracts` | Create a new contract |
| PUT | `/api/LegalContracts/{id}` | Update an existing contract |
| DELETE | `/api/LegalContracts/{id}` | Delete a contract |
| GET | `/api/LegalContracts/latest` | Get recently updated contracts |

### Filtering Contracts

You can filter contracts by creation date using query parameters:

```bash
GET /api/LegalContracts?before=2024-01-01T00:00:00Z&after=2023-01-01T00:00:00Z
```

## Data Model

```csharp
public class LegalContract
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

## Database Context

```csharp
public class LegalContractsDbContext : DbContext
{
    public LegalContractsDbContext(DbContextOptions<LegalContractsDbContext> options) : base(options) { }
    
    public DbSet<LegalContract> Contracts { get; set; } = null!;
}
```

## Adding New Features

### Adding a New Endpoint

1. Add the endpoint to the appropriate controller:

```csharp
[HttpGet("new-endpoint")]
public async Task<ActionResult<IEnumerable<LegalContract>>> GetNewEndpoint()
{
    // Implementation
    return Ok(result);
}
```

### Adding a New Field to Existing Model

1. Add the property to the model:

```csharp
public class LegalContract
{
    // Existing properties
    public string NewField { get; set; } = null!;
}
```

2. Create a new migration:

```bash
dotnet ef migrations add AddNewFieldToContract
```

3. Update the database:

```bash
dotnet ef database update
```

### Adding a New Table/Model

1. Create a new model class in the Models folder
2. Add DbSet to the DbContext:

```csharp
public DbSet<NewModel> NewModels { get; set; } = null!;
```

3. Create and apply migration:

```bash
dotnet ef migrations add AddNewModel
dotnet ef database update
```

## Docker Support

The application includes a Dockerfile for containerization:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "legal-contracts-back.dll"]
```

To build and run with Docker:

```bash
docker build -t legal-contracts-back .
docker run -p 8080:8080 -e ConnectionStrings:DefaultConnection="your_connection_string" legal-contracts-back
```

## Development Notes

- All datetime values are stored and handled in UTC
- Database migrations are automatically applied on application startup
- The application uses Entity Framework Core with the Npgsql PostgreSQL provider
- Swagger documentation is available in development environment at `/swagger`
