#!/bin/bash

# Script ref: https://graspingtech.com/docker-compose-postgresql/

# set -e: Exit immediately if a command has a non-zero exit status
# set -u: Exit immediately if an unset variable is encountered
set -e
set -u

# Set role passwords
export POSTGRES_PASSWORD=$(cat /run/secrets/db_postgres_password)
export MIGRATIONS_PASSWORD=$(cat /run/secrets/db_migrations_password)
export BACKEND_PASSWORD=$(cat /run/secrets/db_backend_password)

# Migrations user and site user ref: https://www.jujens.eu/posts/en/2021/Mar/10/db-user-migrations/
# --set ON_ERROR_STOP=on: Stop script execution if any error occurs
psql --set ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    -- Create login role for healthcheck
    CREATE USER $HEALTHCHECK_ROLE WITH NOINHERIT;

    -- Grant connect privilege to healthcheck role
    GRANT CONNECT ON DATABASE $POSTGRES_DB TO $HEALTHCHECK_ROLE;

    -- Create login role for migrations with database and role creation privileges
    CREATE USER $MIGRATIONS_ROLE WITH CREATEDB CREATEROLE PASSWORD '$MIGRATIONS_PASSWORD';

    -- Create login role for backend with default permissions
    CREATE USER $BACKEND_ROLE WITH PASSWORD '$BACKEND_PASSWORD';

    -- Ensure future tables grant the same permissions to backend role by default
    ALTER DEFAULT PRIVILEGES
    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO $BACKEND_ROLE;

    -- Grant sequence usage for auto-incrementing fields to backend role
    GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO $BACKEND_ROLE;

    -- Ensure future sequences grant the same permissions to backend role by default
    ALTER DEFAULT PRIVILEGES 
    GRANT USAGE, SELECT ON SEQUENCES TO $BACKEND_ROLE;

    -- Revoke grant option for all privileges for backend role by default
    REVOKE GRANT OPTION FOR ALL PRIVILEGES ON ALL TABLES IN SCHEMA public FROM $BACKEND_ROLE;

    -- Ensure future sequences revoke grant option for all priviliges for backend role by default
    ALTER DEFAULT PRIVILEGES 
    REVOKE GRANT OPTION FOR ALL PRIVILEGES ON TABLES FROM $BACKEND_ROLE;

    -- Revoke create tables privilege for all users
    REVOKE CREATE ON SCHEMA public FROM PUBLIC;

    -- Restore create tables privilege to migrations role
    GRANT CREATE ON SCHEMA public TO $MIGRATIONS_ROLE;
EOSQL
