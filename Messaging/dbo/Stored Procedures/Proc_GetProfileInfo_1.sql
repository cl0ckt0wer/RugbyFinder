CREATE PROCEDURE [dbo].[Proc_GetProfileInfo]
	@guid uniqueidentifier
AS
	SELECT TOP (1) RN.Name, RN.Bio
	, COALESCE(DATEDIFF(DAY, RL.RefreshDate, SYSDATETIME()), 888) AS DaysSinceLastLogin
	, COALESCE(DATEDIFF(DAY, RN.CreateDate, SYSDATETIME()), 999) AS DaysSinceCreate
	, c.City
	, T.TeamName
	, T.ID AS [MyTeamId]
	FROM dbo.RuggerName RN 
	LEFT JOIN dbo.RuggerLocation RL ON RL.Id = RN.Id
	LEFT JOIN dbo.Cities C ON C.id = dbo.sfn_GetClosestCityId(rl.Coordinate)
	LEFT JOIN dbo.RuggerTeam RT ON RT.RuggerId = RN.Id
	LEFT JOIN dbo.Teams T ON T.ID = RT.TeamId
	WHERE RN.Id = @guid
RETURN 1