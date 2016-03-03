USE master

-- Cleaning up before we start
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'Listener')
DROP LOGIN Listener

CREATE LOGIN Listener WITH PASSWORD=N'listener', 
            DEFAULT_DATABASE=EF7Context, 
            CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

-- Switch to EF7Context
USE EF7Context

CREATE USER Listener FOR LOGIN Listener 
WITH DEFAULT_SCHEMA = sub
GO

CREATE SCHEMA sub AUTHORIZATION Listener
GO

EXEC sp_addrole 'sql_dependency_starter' 
EXEC sp_addrole 'sql_dependency_subscriber' 

-- Permissions needed for [sql_dependency_starter]
GRANT CREATE PROCEDURE to [sql_dependency_starter] 
GRANT CREATE QUEUE to [sql_dependency_starter]
GRANT CREATE SERVICE to [sql_dependency_starter]
GRANT REFERENCES on 
CONTRACT::[http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]
  to [sql_dependency_starter] 
GRANT VIEW DEFINITION TO [sql_dependency_starter] 

-- Permissions needed for [sql_dependency_subscriber] 
GRANT SELECT to [sql_dependency_subscriber] 
GRANT SUBSCRIBE QUERY NOTIFICATIONS TO [sql_dependency_subscriber] 
GRANT RECEIVE ON QueryNotificationErrorsQueue TO [sql_dependency_subscriber] 
GRANT REFERENCES on 
CONTRACT::[http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]
  to [sql_dependency_subscriber] 

-- Making sure that my users are member of the correct role.
EXEC sp_addrolemember 'sql_dependency_starter', 'Listener'
EXEC sp_addrolemember 'sql_dependency_subscriber', 'Listener'