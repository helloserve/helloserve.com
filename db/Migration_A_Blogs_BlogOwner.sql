DECLARE @Blogs TABLE (FeatureId INT, [Key] NVARCHAR(250), Title NVARCHAR(250), Content NVARCHAR(MAX), IsPublished BIT, PublishDate DATETIME, ImageUrl NVARCHAR(MAX))
INSERT INTO @Blogs (FeatureId, [Key], Title, Content, IsPublished, PublishDate, ImageUrl)
SELECT
    n.FeatureId,
    [Key] = LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(n.Title, ',', ''), '!', ''), '.', ''), '(', ''), ')', ''), '"', ''), '''', ''), ' ', '-')),
    n.Title,
    Content = n.Cut + n.Post,
    n.IsPublished,
    PublishDate = n.CreatedDate,
    ImageUrl = 'media/' + REPLACE(m.[Location], '\', '/')
FROM News n
LEFT
JOIN Media m ON n.HeaderImageId = m.MediaId

MERGE Blogs AS TARGET
USING (SELECT [Key], Title, Content, IsPublished, PublishDate, ImageUrl FROM @Blogs) AS SOURCE
ON TARGET.[Key] = SOURCE.[Key]
WHEN MATCHED THEN
    UPDATE
    SET Title = SOURCE.Title,
        Content = SOURCE.content,
        IsPublished = SOURCE.IsPublished,
        PublishDate = SOURCE.PublishDate,
        ImageUrl = SOURCE.ImageUrl
WHEN NOT MATCHED THEN
    INSERT ([Key], Title, Content, IsPublished, PublishDate, ImageUrl)
    VALUES (SOURCE.[Key], SOURCE.Title, SOURCE.Content, SOURCE.IsPublished, SOURCE.PublishDate, SOURCE.ImageUrl);

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

/*
DELETE BlogOwners
DELETE Blogs
*/