DECLARE @Blogs TABLE (FeatureId INT, [Key] NVARCHAR(250), Title NVARCHAR(250), Content NVARCHAR(MAX), IsPublished BIT, PublishDate DATETIME)
INSERT INTO @Blogs (FeatureId, [Key], Title, Content, IsPublished, PublishDate)
SELECT
    FeatureId,
    [Key] = LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Title, ',', ''), '!', ''), '.', ''), '(', ''), ')', ''), '"', ''), '''', ''), ' ', '_')),
    Title,
    Content = Cut + Post,
    IsPublished,
    PublishDate = CreatedDate
FROM News

MERGE Blogs AS TARGET
USING (SELECT [Key], Title, Content, IsPublished, PublishDate FROM @Blogs) AS SOURCE
ON TARGET.[Key] = SOURCE.[Key]
WHEN MATCHED THEN
    UPDATE
    SET Title = SOURCE.Title,
        Content = SOURCE.content,
        IsPublished = SOURCE.IsPublished,
        PublishDate = SOURCE.PublishDate
WHEN NOT MATCHED THEN
    INSERT ([Key], Title, Content, IsPublished, PublishDate)
    VALUES (SOURCE.[Key], SOURCE.Title, SOURCE.Content, SOURCE.IsPublished, SOURCE.PublishDate);

INSERT INTO BlogOwners (BlogKey, OwnerId, OwnerType)
SELECT [Key], FeatureId, 'Feature' AS OwnerType
FROM @Blogs s
LEFT
JOIN BlogOwners bs ON s.[Key] = bs.BlogKey 
                  AND s.FeatureId = bs.OwnerId
                  AND bs.OwnerType = 'Feature'
WHERE FeatureId IS NOT NULL
  AND bs.BlogOwnerId IS NULL


--SELECT * FROM Blogs
--SELECT * FROM BlogOwners

--DELETE Blogs
--DELETE BlogOwners