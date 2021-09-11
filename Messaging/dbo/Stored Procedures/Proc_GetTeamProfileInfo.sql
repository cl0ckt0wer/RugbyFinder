CREATE PROCEDURE [dbo].[Proc_GetTeamProfileInfo]
	@teamid uniqueidentifier
AS
	SELECT TOP(1) T.Id as TeamId ,T.TeamName, T.TeamBio, t.TeamCityId, P.Pic as TeamPic, T.TeamOwner
	,(SELECT COUNT(*) FROM DBO.RuggerTeam RT WHERE RT.TeamId = @teamid) AS Playercount
	, c.id as CityId
			,C.country, c.admin_name, c.city
		/*, ROW_NUMBER() OVER (ORDER BY C.Coordinates.STDistance(@geo)) AS LOCATIONORDER*/
		FROM Teams T
		JOIN Cities C ON C.id = T.TeamCityId
		LEFT JOIN TeamPic P ON P.Id = T.Id
		WHERE T.Id = @teamid;

RETURN 1