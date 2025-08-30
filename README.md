# Legal Contracts Management System

A full-stack application for managing legal contracts, built with a .NET Core 8 backend, a modern frontend, and PostgreSQL database, all containerized with Docker.

## Project Structure

```text
legal-contracts/
├── legal-contracts-back/          # .NET Core 8 Web API
├── legal-contracts-back.Tests/    # Unit tests for backend
├── legal-contracts-front/         # Frontend application
├── .env                          # Environment variables
├── docker-compose.yml            # Multi-container setup
├── legal-contracts.sln           # Visual Studio solution
└── README.md                     # This file
```

## System Architecture

The application consists of three main services running in Docker containers:

1. **PostgreSQL Database** (v16.3) - Data persistence layer
2. **Backend API** (.NET Core 8) - RESTful API with Entity Framework Core
3. **Frontend Application** - Web interface for contract management

## Prerequisites

- Docker Desktop (or Docker Engine)
- Docker Compose
- Git

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd legal-contracts
   ```

2. **Configure environment variables**
   
   The `.env` file in the root directory configures the database connection:
   ```bash
   # Database configuration
   POSTGRES_DB=mydb
   POSTGRES_USER=postgres
   POSTGRES_PASSWORD=postgres
   ```

3. **Start the application**
   ```bash
   docker-compose up -d
   ```

4. **Access the application**
   - Frontend: http://localhost:3000
   - Backend API: http://localhost:5000
   - Database: localhost:5432

## Services Overview

### Database (PostgreSQL)
- **Container**: `general-db`
- **Port**: 5432
- **Image**: postgres:16.3-alpine
- **Data persistence**: Docker volume `general-db-data`
- **Configuration**: Set via `.env` file

### Backend API
- **Container**: `legal-contracts-backend`
- **Port**: 5000 (external) → 8080 (internal)
- **Technology**: .NET Core 8 with Entity Framework Core
- **Features**:
  - RESTful API endpoints for CRUD operations
  - Automatic database migrations
  - Swagger documentation in development
  - UTC datetime handling

### Frontend Application
- **Container**: `legal-contracts-frontend`
- **Port**: 3000 (external) → 80 (internal)
- **Technology**: Modern JavaScript framework (React/Angular/Vue)
- **Features**: User interface for contract management

## Environment Configuration

The system uses environment variables configured in the root `.env` file:

```bash
# Database settings
POSTGRES_DB=mydb
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres

# Optional: Additional configurations can be added here
```

## Docker Commands

### Start all services
```bash
docker-compose up -d
```

### Stop all services
```bash
docker-compose down
```

### View logs
```bash
docker-compose logs -f
```

### Rebuild containers
```bash
docker-compose up -d --build
```

### Check service status
```bash
docker-compose ps
```

## Database Management

The PostgreSQL database is automatically initialized with:
- Database name: `mydb` (configurable in `.env`)
- Default user: `postgres` (configurable in `.env`)
- Automatic schema migration on backend startup

### Access database directly
```bash
docker exec -it general-db psql -U postgres -d mydb
```

## Development

### Backend Development
- The backend is a .NET Core 8 application
- Uses Entity Framework Core with PostgreSQL provider
- Includes automatic database migrations
- Contains unit tests in the `legal-contracts-back.Tests` project

### Frontend Development
- The frontend is built with a modern JavaScript framework
- Communicates with the backend API via REST
- Hot reload available in development mode

### Running Tests
```bash
# Run backend tests
cd legal-contracts-back.Tests
dotnet test
```

## Production Deployment

For production deployment:
1. Update the `.env` file with secure credentials
2. Set `ASPNETCORE_ENVIRONMENT=Production` in docker-compose.yml
3. Configure proper SSL certificates
4. Set up database backups for the PostgreSQL volume

## Troubleshooting

### Common Issues

1. **Port conflicts**: Ensure ports 3000, 5000, and 5432 are available
2. **Database connection**: Verify PostgreSQL credentials in `.env` file
3. **Container issues**: Use `docker-compose logs` to view error messages

### Reset the application
```bash
docker-compose down -v
docker-compose up -d
```

## AI Use:
The project was created with AI support on multiple aspect, primarly AI was use as an assistance to speed up and know specific points.

Main tools used where:
- ChatGPT
- Copilot
- Deepseek

The way I use the AI agents are asked very specific questions with as much context as I can. Then read carefully what they have answered and then check if it is reasonable or not. Sometimes I double check on google for specific things AI said. When using for coding or documenting, always is reviewed the code, never use copy-paste directly. For documenting I always document myself and then use AI to improve it.

The following information was requested to AI tools:

- **Documentation Creation** (Deepseek): Comprehensive README generation for both backend (.NET Core 8 + Entity Framework) and full-stack application (including frontend + Docker setup) based on pre-documentation created by myself.
- **Technology Comparisons** (ChatGPT): Vue vs React comparison, Vuetify vs Material-UI equivalents, and testing approaches
- **Asistance while doing Vue Frontend** (Copilot): Since I have never developed on Vue, I use AI to help me understanding code and assist me 'translating' from React approach to Vue.
- **Docker Configuration** (Deepseek): Assistance creating Dockerfile for Vue frontend application
- **.NET Core Information** (ChatGPT): LTS version details and Entity Framework migration commands
- **API Documentation** (Copilot): Swagger/OpenAPI implementation guidance for .NET Core
- **Testing** (Copilot): Request for comprehensive test cases for the LegalContractsController API endpoints
- **Database Management** (ChatGPT): PostgreSQL migration commands and best practices with Entity Framework Core