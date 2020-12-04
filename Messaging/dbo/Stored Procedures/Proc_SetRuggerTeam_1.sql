CREATE PROCEDURE [dbo].[Proc_SetRuggerTeam]
	@RuggerId UNIQUEIDENTIFIER,
	@TeamId UNIQUEIDENTIFIER
AS
	DELETE dbo.RuggerTeam
	WHERE RuggerId = @RuggerId;

	INSERT dbo.RuggerTeam (TeamId, RuggerId)
	VALUES (@TeamId, @RuggerId)

RETURN 1