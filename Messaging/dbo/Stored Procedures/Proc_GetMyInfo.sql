CREATE PROCEDURE [dbo].[Proc_GetMyInfo]
	@Uid uniqueidentifier
	--@Name NVARCHAR(50) OUT,
	--@Bio NVARCHAR(MAX) OUT,
	--@TeamName nvarchar(200) OUT,
	--@TeamId UNIQUEIDENTIFIER OUT,
	--@ProfilePic VARBINARY(MAX) OUT
AS
	SET NOCOUNT ON;

	SELECT TOP (1)  N.id AS MyId
	,COALESCE(N.Name, '') AS [MyName] 
	,COALESCE(N.Bio, '') AS [MyBio]
	,P.Pic AS ProfilePic
	,T.Id AS TeamId
	,T.TeamName
	,T.TeamBio
	,TP.Pic AS TeamPic
	,C.id AS CityId
	,C.country
	,C.admin_name
	,C.city
	,C.Coordinates.STDistance(l.Coordinate) AS Distance
	FROM dbo.RuggerName N
	LEFT JOIN dbo.RuggerTeam RT ON RT.RuggerId = N.Id
	LEFT JOIN dbo.Teams T ON T.Id = RT.TeamId
	LEFT JOIN dbo.RuggerPic P ON P.Id = N.Id
	LEFT JOIN dbo.Cities C on C.id = T.TeamCityId
	LEFT JOIN dbo.TeamPic TP ON TP.Id = T.Id
	LEFT JOIN dbo.RuggerLocation L ON l.Id = n.Id
	WHERE N.ID = @Uid

	
RETURN 1