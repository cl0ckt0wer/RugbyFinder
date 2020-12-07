CREATE PROCEDURE [dbo].[Proc_GetMyInfo]
	@Uid uniqueidentifier
	--@Name NVARCHAR(50) OUT,
	--@Bio NVARCHAR(MAX) OUT,
	--@TeamName nvarchar(200) OUT,
	--@TeamId UNIQUEIDENTIFIER OUT,
	--@ProfilePic VARBINARY(MAX) OUT
AS
	SET NOCOUNT ON;

	SELECT TOP (1)  COALESCE(N.Name, '') as [MyName] ,COALESCE(N.Bio, '') as [MyBio], 
	COALESCE(T.TeamName, 'NO TEAM') as [TeamName]
	,COALESCE(T.Id, CONVERT(UNIQUEIDENTIFIER, CONVERT(BINARY, 0))) as [TeamId]
	,COALESCE(P.Pic, CONVERT(VARBINARY(MAX), '')) as ProfilePic
	FROM dbo.RuggerName N
	LEFT JOIN dbo.RuggerTeam RT ON RT.RuggerId = N.Id
	LEFT JOIN dbo.Teams T ON T.Id = RT.TeamId
	LEFT JOIN dbo.RuggerPic P ON P.Id = N.Id
	WHERE N.ID = @Uid

	
RETURN 1