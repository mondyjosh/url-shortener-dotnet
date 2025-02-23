# LinksApi.Web

The **LinksApi.Web** project provides a RESTful web API for URL shortening/retrieval. It acts as the entry point for external consumers.

## Directory Structure

```
└──📁 src
    └──📁 LinksApi.Web
        ├──📁 Controllers                       # Directory for HTTP controllers
        ├──📁 wwwroot/openapi/v1/**             # wwwroot contains OpenAPI (Swagger) documentation files
        ├── appsettings.json                    # App config
        ├── appsettings.Development.json        # Development environment-specific app config        
        └── Program.cs                          # Entry point for the web app
```

## App Design Guidelines

**This should be a thin application that should focus only on external access as much as possible.**

If there is any business logic (i.e., "stuff that does work"), it should belong in [`LinksApi`](../LinksApi/).

## Running the App

You can run the `LinksApi.Web` project using either `dotnet` commands, Docker, or Docker Compose.

> [!WARNING]  
> Running the app without Docker Compose requires the database that services this web API project.
> Running the **`migrations`** service found in [`compose.yml`](../../compose.yml) will start the database and run any migrations.
> (`migrations` depends on `db`, so both services will be run).
>
> ```sh
> # Navigate to project root directory
> cd ../.. 
>
> # Run Docker Compose for db + migrations (migrations depends on db)
> docker compose up -d migrations
> ```

## Run with Docker Compose

To run the app with Docker Compose, refer to the [root README](../../README.md#building-and-running-the-application).

This approach includes starting the PostgreSQL container prior to launching the .NET app, so no additional database steps are required.

### Run with .NET CLI

To run the app with the .NET CLI, run:

```sh
# Navigate to app directory
cd src/LinksApi.Web

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application:
dotnet run
```

The application will be available at <http://localhost:5286> or at <https://localhost:7269> if using HTTPS.

## Run with Docker

If desired, the app can be built and ran with Docker. First, ensure Docker is installed and running on your machine.

To run the app with Docker, run:

```sh
# Navigate to project root directory
cd ../..

# Build the Docker image
docker build -t linksapi-web .

# Run the container
docker run -p 8000:80 linksapi-web
```

The application will be available at <http://localhost:8000>.
