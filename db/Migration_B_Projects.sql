DECLARE @Projects TABLE ([Key] NVARCHAR(150), Name NVARCHAR(250), ComponentPage NVARCHAR(250), IsActive BIT)

INSERT INTO @Projects ([Key], Name, ComponentPage, IsActive)
VALUES ('stingrayincursion', 'Stingray Incursion', 'StingrayIncursion', 1),
	   ('thebluecar', 'The Blue Car', 'TheBlueCar', 1),
	   ('thesarienexperiment', 'The Sarien Experiment', 'SarienExperiment', 1)

MERGE Projects AS target
USINg (SELECT [Key], Name, ComponentPage, IsActive FROM @Projects) AS source
ON target.[Key] = source.[Key]
WHEN MATCHED THEN
	UPDATE
	SET Name = source.Name,
		ComponentPage = source.ComponentPage,
		IsActive = source.IsActive
WHEN NOT MATCHED THEN
	INSERT ([Key], Name, ComponentPage, IsActive)
	VALUES (source.[Key], source.Name, source.ComponentPage, source.IsActive);


UPDATE bo
SET OwnerKey = p.[Key],
    OwnerType = 'Project'
FROM BlogOwners bo
JOIN Feature f ON bo.OwnerId = f.FeatureId
LEFT
JOIN Projects p ON f.Name = p.Name
WHERE bo.OwnerType = 'Feature'
