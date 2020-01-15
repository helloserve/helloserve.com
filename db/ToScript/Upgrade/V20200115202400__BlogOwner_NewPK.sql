DELETE BlogOwners WHERE OwnerKey IS NULL

ALTER TABLE BlogOwners
ALTER COLUMN OwnerKey NVARCHAR(250) NOT NULL
GO

IF EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'BlogOwners' AND c.name = 'BlogOwnerId')
BEGIN
  ALTER TABLE BlogOwners
  DROP CONSTRAINT PK_BlogOwners

  ALTER TABLE BlogOwners
  DROP COLUMN BlogOwnerId

  ALTER TABLE BlogOwners
  ADD CONSTRAINT PK_BlogOwners PRIMARY KEY CLUSTERED (BlogKey ASC, OwnerKey ASC) ON [PRIMARY]
END

IF EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'BlogOwners' AND c.name = 'OwnerId')
ALTER TABLE BlogOwners
DROP COLUMN OwnerId
GO
