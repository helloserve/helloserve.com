IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Projects')
CREATE TABLE Projects
(
  [Key] NVARCHAR(250) NOT NULL,
  Name NVARCHAR(250) NOT NULL,
  ComponentPage NVARCHAR(250) NOT NULL,
  IsActive BIT NOT NULL,  
  CONSTRAINT PK_Projects PRIMARY KEY NONCLUSTERED ([Key]) ON [PRIMARY]
)
ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'BlogOwners' AND c.name = 'OwnerKey')
BEGIN
  ALTER TABLE BlogOwners
  ADD OwnerKey NVARCHAR(250) NULL
END
