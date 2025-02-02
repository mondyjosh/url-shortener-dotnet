# interviews.2025.ascribe
This is a technical interview project for Ascribe in January 2025.

## Technical Challenge Details

See [LRES-Ascribe Technical Challenge](docs/Ascribe_Technical_Challenge.md) for details.

## Building and Running the Application

This application relies on Docker and Docker Compose. 

Start the application by running the following `docker compose` command inside the `/interviews.2025.ascribe` project directory.

```sh
docker compose up --build
```

Alternatively, run the project detached from the terminal by adding the `-d` option

```sh
docker compose up --build
```

The application will be available at <http://localhost:8081>. 

Feel free to test API calls with the Swagger UI!