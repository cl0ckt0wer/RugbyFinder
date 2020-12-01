CREATE PROCEDURE [dbo].[Proc_GetMyInfo]
	@Uid uniqueidentifier,
	@Name NVARCHAR(50) OUT,
	@Bio NVARCHAR(MAX) OUT,
	@TeamName nvarchar(200) OUT,
	@TeamId UNIQUEIDENTIFIER OUT
AS
	SET NOCOUNT ON;

	SELECT TOP (1) @NAME = COALESCE(N.Name, ''), @Bio = COALESCE(N.Bio, ''), 
	@TeamName = COALESCE(T.TeamName, 'NO TEAM')
	,@TeamId = COALESCE(T.Id, CONVERT(UNIQUEIDENTIFIER, CONVERT(BINARY, 0)))
	FROM dbo.RuggerName N
	LEFT JOIN dbo.RuggerTeam RT ON RT.RuggerId = N.Id
	LEFT JOIN dbo.Teams T ON T.Id = RT.TeamId
	WHERE N.ID = @Uid
RETURN 1
