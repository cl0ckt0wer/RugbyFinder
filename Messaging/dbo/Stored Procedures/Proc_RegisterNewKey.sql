CREATE PROCEDURE [dbo].[Proc_RegisterNewKey]
	@apikey varchar(64)
AS
	INSERT dbo.KeyGuid (Id, [Key])
	OUTPUT inserted.Id AS MyGuid, inserted.[Key]
	VALUES (NEWID(), @apikey)