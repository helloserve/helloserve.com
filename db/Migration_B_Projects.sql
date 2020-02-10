DECLARE @Projects TABLE ([Key] NVARCHAR(150), Name NVARCHAR(250), ComponentPage NVARCHAR(250), IsActive BIT, SortOrder INT, Description NVARCHAR(MAX), ImageUrl NVARCHAR(500))

INSERT INTO @Projects ([Key], Name, ComponentPage, IsActive, SortOrder, Description, ImageUrl)
VALUES ('stingrayincursion', 'Stingray Incursion', 'StingrayIncursion', 1, 3, 'This was a serious and real attempt at making a game. I wanted to build something I wanted to play.', 'media/Stingray_FeatureHeader.png'),
	     ('thebluecar', 'The Blue Car', 'TheBlueCar', 1, 1, 'Affectionally referred to by its colour, this 1989 Eunos Roadster is my first project car, and has a significant bond with our family.', 'media/20180609_162848.jpg'),
	     ('thesarienexperiment', 'The Sarien Experiment', NULL, 1, 2, NULL, NULL)

MERGE Projects AS target
USINg (SELECT [Key], Name, ComponentPage, IsActive, SortOrder, Description, ImageUrl FROM @Projects) AS source
ON target.[Key] = source.[Key]
WHEN MATCHED THEN
	UPDATE
	SET Name = source.Name,
		ComponentPage = source.ComponentPage,
		IsActive = source.IsActive,
		SortOrder = source.SortOrder,
		Description = source.Description,
		ImageUrl = source.ImageUrl
WHEN NOT MATCHED THEN
	INSERT ([Key], Name, ComponentPage, IsActive, SortOrder, Description, ImageUrl)
	VALUES (source.[Key], source.Name, source.ComponentPage, source.IsActive, source.SortOrder, source.Description, source.ImageUrl);


IF EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id AND t.name = 'BlogOwners' AND c.name = 'OwnerId')
EXEC('
UPDATE bo
SET OwnerKey = p.[Key],
    OwnerType = ''Project''
FROM BlogOwners bo
JOIN Feature f ON bo.OwnerId = f.FeatureId
LEFT
JOIN Projects p ON f.Name = p.Name
WHERE bo.OwnerType = ''Feature''
');