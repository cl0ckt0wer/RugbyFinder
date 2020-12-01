CREATE PROCEDURE [dbo].[Get_TeamProfileInfo]
	@teamid uniqueidentifier
AS
	SELECT TOP(1) T.Id as TeamId ,T.TeamName, T.TeamBio, t.TeamCityId, c.id as CityId
			,C.country, c.admin_name, c.city
		/*, ROW_NUMBER() OVER (ORDER BY C.Coordinates.STDistance(@geo)) AS LOCATIONORDER*/
		FROM Teams T
		JOIN Cities C ON C.id = T.TeamCityId
		WHERE T.Id = @teamid;
RETURN 0
