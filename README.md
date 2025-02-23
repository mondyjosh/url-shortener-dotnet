# interviews.2025.ascribe (archived)

>[!WARNING]
>🚨 **This repository is archived and no longer maintained.** 🚨
>
>This project is no longer actively developed, as per https://github.com/mondyjosh/interviews.2025.ascribe/pull/16.
>
>The repository remains available for reference, but no further updates or support will be provided.
>
>**Note to future me:** Please use this repo as a basis for future technical take-home projects!

This is a technical interview project for Ascribe in January 2025.

This project provides a web API for URL shortening/retrieval, built with .NET 8 and PostgreSQL.

## Technical Challenge Details

See [LRES-Ascribe Technical Challenge](docs/Ascribe_Technical_Challenge.md) for details on project requirements and goals.

## Directory Structure

```
├──📁 db
│    ├── init/init-db.sh    # Custom script for setting up roles and permissions
│    └── migrations         # Directory containing migration scripts ran by Flyway
├──📁 docs                  # Contains project documentation
├──📁 secrets               # Secrets for Docker Compose
├──📁 src
│   ├──📁 LinksApi          # Service library that contains URL shortening/retrieval core business logic 
│   └──📁 LinksApi.Web      # Web API for URL shortening/retrieval
├──📁 test
│    └──📁 LinksApi.Tests   # Test project for LinksApi core business logic
├── .env                    # Contains environment variables for Docker Compose
├── Dockerfile              # Defines the containerization setup for the LinksApi.Web app
└── compose.yml             # Defines and orchestrates containers for the full application (db, migrations, backend)
```

## Building and Running the Application

This application relies on Docker and Docker Compose. Ensure you have the following installed:

- **[Docker](https://docs.docker.com/engine/install/)** (latest version recommended)
- **[Docker Compose](https://docs.docker.com/compose/)** (V2.x or later)

Start the application by running `docker compose` inside the `/interviews.2025.ascribe` project directory:

```sh
docker compose up --build
```

Alternatively, run the project detached from the terminal by adding the `-d` option:

```sh
docker compose up --build -d
```

The application will be available at <http://localhost:8080>.

Feel free to test API calls with the [Swagger UI](http://localhost:8080/swagger)!
