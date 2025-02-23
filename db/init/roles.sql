-- Dedicated user setup for migrations user and site user originally sourced from:
-- https://www.jujens.eu/posts/en/2021/Mar/10/db-user-migrations/

-- Create login role for healthcheck
CREATE USER healthcheck 
WITH 
    NOINHERIT;

-- Grant connect privilege to healthcheck role
GRANT 
    CONNECT 
ON
DATABASE postgres
TO healthcheck;

-- Create login role for Flyway migrations with database and role creation privileges
CREATE USER migrations_flyway
WITH
    CREATEDB 
	CREATEROLE
	PASSWORD 'development_migrations';

-- Create login role for LinksApi.Web backend with default permissions
CREATE USER backend_linksapiweb
	PASSWORD 'development_backend';

-- Grant necessary permissions to backend role in the public schema
GRANT
    SELECT,
	INSERT,
	UPDATE,
	DELETE
	ON ALL TABLES IN SCHEMA public 
to backend_linksapiweb;

-- Ensure future tables grant the same permissions to backend role by default
ALTER DEFAULT PRIVILEGES 
GRANT
    SELECT,
	INSERT,
	UPDATE,
	DELETE
	ON TABLES 
TO backend_linksapiweb;

-- Grant sequence usage for auto-incrementing fields to backend role
GRANT 
    USAGE,
    SELECT
	ON ALL SEQUENCES IN SCHEMA public 
TO backend_linksapiweb;

-- Ensure future sequences grant the same permissions to backend role by default
ALTER DEFAULT PRIVILEGES 
GRANT
    USAGE,
    SELECT
	ON SEQUENCES 
TO backend_linksapiweb;

-- Revoke option for backend role by default
REVOKE GRANT OPTION 
FOR ALL PRIVILEGES 
ON ALL TABLES IN SCHEMA public
FROM backend_linksapiweb;

--  Ensure future sequences revoke option for backend role by default
ALTER DEFAULT PRIVILEGES 
REVOKE GRANT OPTION 
FOR ALL PRIVILEGES ON TABLES
FROM backend_linksapiweb;

-- Revoke create tables privilege for all users
REVOKE 
    CREATE 
ON
SCHEMA public
FROM
PUBLIC;

-- Restore create tables privilege to Flyway migrations role
GRANT 
    CREATE 
ON
SCHEMA public 
TO migrations_flyway;