# interviews.2025.ascribe

This is a technical interview project for Ascribe in January 2025.

This project provides a web API for URL shortening/retrieval, built with .NET 8 and PostgreSQL.

## Technical Challenge Details

See [LRES-Ascribe Technical Challenge](docs/Ascribe_Technical_Challenge.md) for details on project requirements and goals.

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
