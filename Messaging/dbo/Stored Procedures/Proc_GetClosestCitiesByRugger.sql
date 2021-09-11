CREATE PROCEDURE [dbo].[Proc_GetClosestCitiesByRugger] @Key VARCHAR(64)
AS
BEGIN
	DECLARE @Geo GEOGRAPHY;
	DECLARE @myguid UNIQUEIDENTIFIER;

	SELECT @myguid = Id
	FROM KeyGuid
	WHERE [Key] = @Key;

	SELECT TOP (1) @Geo = Coordinate
	FROM RuggerLocation R
	WHERE R.Id = @myguid;

	IF @Geo IS NULL
	BEGIN
		RETURN (- 1);
	END;

	WITH CTE
	AS (
		SELECT TOP (100) C.*
		FROM Cities C
		WHERE C.Coordinates.STDistance(@geo) IS NOT NULL
		ORDER BY C.Coordinates.STDistance(@geo)
		)
	SELECT CTE.country
		,CTE.admin_name
		,CTE.Coordinates.STDistance(@geo)
		,CTE.Id AS CityId
		,CTE.city
	FROM CTE;

	RETURN 1;
END