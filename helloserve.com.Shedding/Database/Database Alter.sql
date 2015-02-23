IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = 'CreatedDate' AND object_id = OBJECT_ID(N'User'))
BEGIN
	ALTER TABLE [User]
	ADD CreatedDate DATETIME NULL
	
	EXEC ('UPDATE [User] SET CreatedDate = GETUTCDATE() WHERE CreatedDate IS NULL')

	ALTER TABLE [User]
	ALTER COLUMN CreatedDate DATETIME NOT NULL
END
GO


IF EXISTS (SELECT * FROM sys.columns WHERE Name = 'PhoneNumber' AND object_id = OBJECT_ID(N'User'))
BEGIN
	EXEC sp_rename @objname = '[User].PhoneNumber', @newname = 'UniqueNumber', @objtype = 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM sys.columns WHERE Name = 'PushNotificationId' AND object_id = OBJECT_ID(N'User'))
BEGIN
	ALTER TABLE [User]
	DROP COLUMN PushNotificationId
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE Name = 'UserPushRegistration' AND type = N'U')
BEGIN
	CREATE TABLE dbo.UserPushRegistration
		(
		Id int NOT NULL IDENTITY (1, 1),
		UserId int NOT NULL,
		PushRegistrationId nvarchar(MAX) NOT NULL
		)

	ALTER TABLE dbo.UserPushRegistration ADD CONSTRAINT
		PK_UserPushRegistration PRIMARY KEY CLUSTERED 
		(
		Id
		)


	ALTER TABLE dbo.UserPushRegistration ADD CONSTRAINT
		FK_UserPushRegistration_User FOREIGN KEY
		(
		UserId
		) REFERENCES dbo.[User]
		(
		Id
		) ON UPDATE  NO ACTION 
		 ON DELETE  NO ACTION 	
END
GO


IF EXISTS (SELECT * FROM 
