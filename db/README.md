# LinksApi Database Migrations

This directory contains the database migration setup and initialization scripts for managing PostgreSQL schema changes using Flyway and a custom initialization script.

## Overview

- **[Flyway](https://documentation.red-gate.com/fd)**: Handles versioned database migrations.
- **/db/init/init-db.sh**: A shell script that sets up initial database roles and privileges.
- **Secrets:** Passwords for different database roles are sourced via `compose.yml` when Docker Compose runs.

## Directory Structure

```
└──📁 db
    ├── init/init-db.sh  # Custom script for setting up roles and permissions
    └── migrations       # Directory containing migration scripts ran by Flyway
```

The `/migrations` directory contains all migrations to run in numbered sequence. Future migrations belong here!

## Setup & Usage

### 1. Database Role Initialization

The `init-db.sh` script:

- Creates service roles (e.g., migrations, backend, healthcheck). Credentials are referenced through `compose.yml`.
- Grants and restricts privileges as appropriate for service roles.
- Restricts unwanted privileges.

This script runs automatically as part of the database initialization when the `db` service starts.

### 2. Flyway Migrations

Flyway applies versioned migration scripts in `/db/migrations/` to the database, ensuring schema consistency.

Migrations are executed by the `migrations` service in `compose.yml`. Credentials are referenced as secrets.

## Running Migrations Manually

To apply migrations manually, run:

```
docker compose run --rm migrations migrate
```

## Notes

- Ensure secrets are set up correctly in `compose.yml` to provide the necessary passwords.
- Flyway migration scripts should follow a consistent naming pattern (e.g., `V1__init.sql`, `V2__add_table.sql`).
- Changes to roles and permissions should be done via `init-db.sh` rather than Flyway migrations.
