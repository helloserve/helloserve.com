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


IF EXISTS (SELECT * FROM sys.columns where Name = 'StartTime' AND object_id = OBJECT_ID(N'ScheduleCalendar') AND user_type_id = TYPE_ID(N'decimal'))
BEGIN
	ALTER TABLE ScheduleCalendar
	ALTER COLUMN StartTime DATETIME NOT NULL
END
GO

IF EXISTS (SELECT * FROM sys.columns where Name = 'EndTime' AND object_id = OBJECT_ID(N'ScheduleCalendar') AND user_type_id = TYPE_ID(N'decimal'))
BEGIN
	ALTER TABLE ScheduleCalendar
	ALTER COLUMN EndTime DATETIME NOT NULL
END
GO

IF EXISTS (SELECT * FROM sys.columns where Name = 'Date' AND object_id = OBJECT_ID(N'ScheduleCalendar'))
BEGIN
	ALTER TABLE ScheduleCalendar
	DROP COLUMN Date
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes where Name = 'IX_ScheduleCalendar_StartTime')
BEGIN
	CREATE NONCLUSTERED INDEX IX_ScheduleCalendar_StartTime ON ScheduleCalendar (StartTime ASC)

END
GO