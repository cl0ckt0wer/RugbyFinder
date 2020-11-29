CREATE PROCEDURE [dbo].[Proc_GetProfileInfo]
	@guid uniqueidentifier
AS
	SELECT TOP (1) RN.Name, RN.Bio
	, COALESCE(DATEDIFF(DAY, RL.RefreshDate, SYSDATETIME()), 888) AS DaysSinceLastLogin
	, COALESCE(DATEDIFF(DAY, RN.CreateDate, SYSDATETIME()), 999) AS DaysSinceCreate
	, c.City
	FROM DBO.RuggerName RN 
	LEFT JOIN DBO.RuggerLocation RL ON RL.Id = RN.Id
	LEFT JOIN DBO.Cities C ON C.id = dbo.sfn_GetClosestCityId(rl.Coordinate)
	WHERE RN.Id = @guid
RETURN 1
