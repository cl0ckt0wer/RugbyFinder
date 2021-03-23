CREATE PROCEDURE [dbo].[Proc_GetClosestTeamsByRugger] @key VARCHAR(64)
AS
BEGIN
	DECLARE @GUID UNIQUEIDENTIFIER;
	DECLARE @Geo GEOGRAPHY;

	SELECT TOP (1) @GUID = Id
	FROM KeyGuid
	WHERE [Key] = @key;

	SELECT TOP (1) @Geo = Coordinate
	FROM RuggerLocation R
	WHERE R.Id = @GUID;

	-- 	WITH CTE AS (
	--	SELECT TOP(100) T.Id as TeamId ,T.TeamName, T.TeamBio, t.TeamCityId, P.Pic AS TeamPic, c.Id as CityId
	--		,C.country, c.admin_name, c.city, c.Coordinates.STDistance(@geo) as Distance
	--	/*, ROW_NUMBER() OVER (ORDER BY C.Coordinates.STDistance(@geo)) AS LOCATIONORDER*/
	--	FROM Teams T
	--	LEFT JOIN Cities C ON C.id = T.TeamCityId
	--	LEFT JOIN TeamPic P ON P.Id = T.Id
	--	WHERE C.Coordinates.STDistance(@geo) IS NOT NULL  
	--	ORDER BY C.Coordinates.STDistance(@geo))
	--SELECT *
	--FROM CTE;
	SELECT TOP (100) T.Id AS TeamId
		,T.TeamName
		,T.TeamBio
		,t.TeamCityId
		,P.Pic AS TeamPic
		,c.id AS CityId
		,C.country
		,c.admin_name
		,c.city
		,c.Coordinates.STDistance(@geo) AS Distance
	FROM Teams T
	LEFT JOIN Cities C ON C.id = T.TeamCityId
	LEFT JOIN TeamPic P ON P.Id = T.Id
	WHERE C.Coordinates.STDistance(@geo) IS NOT NULL
	ORDER BY C.Coordinates.STDistance(@geo)

	RETURN 1;
END
