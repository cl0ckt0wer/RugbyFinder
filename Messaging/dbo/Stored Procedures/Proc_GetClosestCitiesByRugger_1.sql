CREATE PROCEDURE [dbo].[Proc_GetClosestCitiesByRugger]
	@myguid uniqueidentifier
AS
BEGIN
	DECLARE @Geo GEOGRAPHY;
	SELECT TOP (1) @Geo = Coordinate
	FROM RuggerLocation R
	WHERE R.Id = @myguid;

	 	WITH CTE AS (
		SELECT TOP(100) C.*
		FROM  Cities C
		WHERE C.Coordinates.STDistance(@geo) IS NOT NULL  
		ORDER BY C.Coordinates.STDistance(@geo))

	SELECT CTE.country, CTE.admin_name, CTE.Coordinates.STDistance(@geo), CTE.Id as CityId, CTE.city
	FROM CTE;
	

RETURN 1;
END