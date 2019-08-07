IF NOT EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'Blogs' AND c.name = 'Description')
ALTER TABLE Blogs
ADD [Description] NVARCHAR(500) NULL,
    [ImageUrl] NVARCHAR(500) NULL,
    [Type] NVARCHAR(50) NULL
GO
