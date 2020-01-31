IF EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'Projects' AND c.name = 'ComponentPage')
ALTER TABLE Projects
ALTER COLUMN ComponentPage NVARCHAR(250) NULL
GO
