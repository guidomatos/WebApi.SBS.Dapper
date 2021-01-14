--GRANT CONNECT,RESOURCE,UNLIMITED TABLESPACE TO blog IDENTIFIED BY blog;

--Create user and password

alter session set "_ORACLE_SCRIPT"=true;
CREATE USER gmatos IDENTIFIED BY gmatosc88;

--Assign privileges to the user through attaching the account to various roles, 
--starting with the CONNECT role:

GRANT CONNECT TO gmatos;

--Assign role RESOURCE, DBA

GRANT CONNECT, RESOURCE, DBA TO gmatos;

--connect to the database and create a session using GRANT CREATE SESSION
--We’ll also combine that with all privileges using GRANT ANY PRIVILEGES.

GRANT CREATE SESSION GRANT ANY PRIVILEGE TO gmatos;

--We also need to ensure our new user has disk space allocated 
--in the system to actually create or modify tables and data, 
--so we’ll GRANT TABLESPACE like so:
GRANT UNLIMITED TABLESPACE TO gmatos;
