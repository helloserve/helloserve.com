IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'Projects' AND c.name = 'SortOrder')
ALTER TABLE Projects
ADD SortOrder INT NULL
GO

EXEC ('UPDATE Projects SET SortOrder = 0 WHERE SortOrder IS NULL')
GO

ALTER TABLE Projects
ALTER COLUMN SortOrder INT NOT NULL
GO
