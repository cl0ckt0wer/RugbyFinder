CREATE PROCEDURE [dbo].[Proc_GetClosestTeamsByRugger]
	@myguid uniqueidentifier
AS
BEGIN
	DECLARE @Geo GEOGRAPHY;
	SELECT TOP (1) @Geo = Coordinate
	FROM RuggerLocation R
	WHERE R.Id = @myguid;

	 	WITH CTE AS (
		SELECT TOP(100) T.Id as TeamId ,T.TeamName, T.TeamBio, t.TeamCityId, c.id as CityId
			,C.country, c.admin_name, c.city, c.Coordinates.STDistance(@geo) as Distance
		/*, ROW_NUMBER() OVER (ORDER BY C.Coordinates.STDistance(@geo)) AS LOCATIONORDER*/
		FROM Teams T
		JOIN Cities C ON C.id = T.TeamCityId
		WHERE C.Coordinates.STDistance(@geo) IS NOT NULL  
		ORDER BY C.Coordinates.STDistance(@geo))

	SELECT *
	FROM CTE;
	

RETURN 0;
END