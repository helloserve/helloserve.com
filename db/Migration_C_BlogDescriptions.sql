WITH BlogSource AS
(
	SELECT
	CASE 
	WHEN PATINDEX('%<p>%', Content) = 1
	THEN SUBSTRING(Content, 0, PATINDEX('%</p>%', Content) + 4)
	WHEN PATINDEX('%<a href=%', SUBSTRING(Content, 0, PATINDEX('%.%', Content))) > 0
	THEN SUBSTRING(Content, 0, PATINDEX('%</a>%', Content)) + SUBSTRING(Content, PATINDEX('%</a>%', Content), PATINDEX('%.%', SUBSTRING(Content, PATINDEX('%</a>%', Content), 1000)))
	WHEN PATINDEX('%<p>%', SUBSTRING(Content, 0, PATINDEX('%.%', Content))) >= 1
	THEN SUBSTRING(Content, 0, PATINDEX('%</p>%', Content) + 4)
	ELSE SUBSTRING(Content, 0, PATINDEX('%.%', Content) + 1)
	END AS FromContent,
	[Key]
	FROM Blogs 
	WHERE Description IS NULL
),
CleanSource AS
(
	SELECT
		REPLACE(REPLACE(REPLACE(REPLACE(FromContent, '<p>', ''), '</p>', ''), '<h2>', ''), '</h2>', '') AS FromContent,
		[Key]
	FROM BlogSource
)
UPDATE b
SET Description = 
--select
	CASE WHEN LEN(s.FromContent) >= 500
	THEN SUBSTRING(s.FromContent, 0, 495) + '...'
	ELSE s.FromContent
	END
FROM Blogs b
JOIN CleanSource s ON b.[Key] = s.[Key]
