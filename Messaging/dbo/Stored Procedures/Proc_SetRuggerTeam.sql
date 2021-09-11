CREATE PROCEDURE [dbo].[Proc_SetRuggerTeam]
	@Key VARCHAR(64),
	@TeamId UNIQUEIDENTIFIER
AS
	DECLARE @RUGGERID UNIQUEIDENTIFIER;
	SELECT @RUGGERID = Id
	FROM KeyGuid
	WHERE [Key] = @Key;

	DELETE DBO.RuggerTeam
	WHERE RuggerId = @RuggerId;

	INSERT DBO.RuggerTeam (TeamId, RuggerId)
	VALUES (@TeamId, @RuggerId)

RETURN 1