DECLARE @Projects TABLE ([Key] NVARCHAR(150), Name NVARCHAR(250), ComponentPage NVARCHAR(250), IsActive BIT, SortOrder INT)

INSERT INTO @Projects ([Key], Name, ComponentPage, IsActive, SortOrder)
VALUES ('stingrayincursion', 'Stingray Incursion', 'StingrayIncursion', 1, 3),
	   ('thebluecar', 'The Blue Car', 'TheBlueCar', 1, 1),
	   ('thesarienexperiment', 'The Sarien Experiment', 'SarienExperiment', 1, 2)

MERGE Projects AS target
USINg (SELECT [Key], Name, ComponentPage, IsActive, SortOrder FROM @Projects) AS source
ON target.[Key] = source.[Key]
WHEN MATCHED THEN
	UPDATE
	SET Name = source.Name,
		ComponentPage = source.ComponentPage,
		IsActive = source.IsActive,
		SortOrder = source.SortOrder
WHEN NOT MATCHED THEN
	INSERT ([Key], Name, ComponentPage, IsActive, SortOrder)
	VALUES (source.[Key], source.Name, source.ComponentPage, source.IsActive, source.SortOrder);


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