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
