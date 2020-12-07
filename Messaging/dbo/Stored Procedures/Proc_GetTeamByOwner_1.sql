CREATE PROCEDURE [dbo].[Proc_GetTeamByOwner]
	@OwnerGuid UNIQUEIDENTIFIER
AS
	DECLARE @GEO GEOGRAPHY;

	SELECT TOP 1 @GEO = L.Coordinate
	FROM dbo.RuggerLocation l
	WHERE L.Id = @OwnerGuid;

		SELECT TOP(1) T.Id as TeamId ,T.TeamName, T.TeamBio, t.TeamCityId, T.TeamOwner as RuggerOwner,P.Pic AS TeamPic, c.Id as CityId
			,C.country, c.admin_name, c.city, c.Coordinates.STDistance(@geo) as Distance
		/*, ROW_NUMBER() OVER (ORDER BY C.Coordinates.STDistance(@geo)) AS LOCATIONORDER*/
		FROM Teams T
		LEFT JOIN Cities C ON C.Id = T.TeamCityId
		LEFT JOIN TeamPic P ON P.Id = T.Id
	WHERE T.TeamOwner = @OwnerGuid;

RETURN 1