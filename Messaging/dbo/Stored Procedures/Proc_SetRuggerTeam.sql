CREATE PROCEDURE [dbo].[Proc_SetRuggerTeam]
	@RuggerId UNIQUEIDENTIFIER,
	@TeamId UNIQUEIDENTIFIER
AS
	DELETE DBO.RuggerTeam
	WHERE RuggerId = @RuggerId;

	INSERT DBO.RuggerTeam (TeamId, RuggerId)
	VALUES (@TeamId, @RuggerId)

RETURN 1
