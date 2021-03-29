CREATE PROCEDURE [dbo].[Proc_GetMyInfo]
	@key varchar(64)
AS
	SET NOCOUNT ON;
	DECLARE @UID UNIQUEIDENTIFIER;
	SELECT @UID = Id
	FROM KeyGuid
	WHERE [Key] = @key;

	SELECT TOP (1)  N.id AS MyId
	,COALESCE(N.Name, '') AS [MyName] 
	,COALESCE(N.Bio, '') AS [MyBio]
	,P.Pic AS ProfilePic
	,(SELECT TOP 1 ID FROM DBO.Teams WHERE TeamOwner = @UID) AS TeamOwned
	,V.[FileName] as VideoFile
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
	LEFT JOIN dbo.RuggerVideo V ON V.Id = N.Id
	WHERE N.ID = @Uid

	
RETURN 1