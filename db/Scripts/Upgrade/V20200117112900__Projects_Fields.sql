IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'Projects' AND c.name = 'Description')
ALTER TABLE Projects
ADD Description NVARCHAR(MAX) NULL,
    ImageUrl NVARCHAR(250) NULL
GO
