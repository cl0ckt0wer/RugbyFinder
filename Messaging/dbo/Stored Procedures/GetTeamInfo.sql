CREATE PROCEDURE [dbo].[GetTeamInfo]
	@id uniqueidentifier
AS
	SELECT t.Id as [TeamId], t.TeamBio, t.TeamCityId, t.TeamName
	FROM dbo.Teams t
	WHERE Id = @id

RETURN 1