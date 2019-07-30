IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '$(env_database_name)')
  CREATE database $(env_database_name);
GO
 -- Create the default dB user
USE [master]
GO
IF NOT EXISTS(SELECT 1 FROM $(env_database_name).sys.database_principals WHERE name = '$(env_database_name)')
CREATE LOGIN $(env_login_name) WITH PASSWORD=N'$(env_login_password)', DEFAULT_DATABASE=$(env_database_name), CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE $(env_database_name)
GO
IF NOT EXISTS(SELECT 1 FROM $(env_database_name).sys.database_principals WHERE name = '$(env_database_name)')
 BEGIN
   CREATE USER $(env_login_user) FOR LOGIN $(env_login_name)
   ALTER USER $(env_login_user) WITH DEFAULT_SCHEMA=[dbo]
   ALTER ROLE [db_owner] ADD MEMBER $(env_login_user)
 END
GO
