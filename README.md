# URL Shortener - .NET 

A simple web API for URL shortening/retrieval, built with .NET 8 and PostgreSQL.

## Overview

This project is a basic link shortening service, similar in concept to services like [bit.ly](https://bit.ly).  
It includes an interface to input a URL and display the shortened version after submission.

The API provides two main functions:

1. Shorten a URL into a brief alphanumeric string.
2. Expand a shortened string back to the original URL, returning an error if it doesn’t exist.

It includes a simple storage mechanism to persist URLs and supports local development and testing with minimal configuration.

## Directory Structure

```
├──📁 db
│    ├── init/init-db.sh    # Custom script for setting up roles and permissions
│    └── migrations         # Directory containing migration scripts ran by Flyway
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

Start the application by running `docker compose` inside the project directory root:

```sh
docker compose up --build
```

Alternatively, run the project detached from the terminal by adding the `-d` option:

```sh
docker compose up --build -d
```

The application will be available at <http://localhost:8080>.

Feel free to test API calls with the [Swagger UI](http://localhost:8080/swagger)!
