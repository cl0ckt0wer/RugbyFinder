CREATE PROCEDURE [dbo].[Proc_SetRuggerTeam]
	@Key VARCHAR(64),
	@TeamId UNIQUEIDENTIFIER
AS
	DECLARE @RUGGERID UNIQUEIDENTIFIER;
	SELECT @RUGGERID = Id
	FROM KeyGuid
	WHERE [Key] = @Key;

	DELETE dbo.RuggerTeam
	WHERE RuggerId = @RuggerId;

	INSERT dbo.RuggerTeam (TeamId, RuggerId)
	VALUES (@TeamId, @RuggerId)

RETURN 1