/*
The database must have a MEMORY_OPTIMIZED_DATA filegroup
before the memory optimized object can be created.
*/

CREATE PROCEDURE [dbo].[Proc_GetRuggerMessage]
	@key VARCHAR(64),
	@theirid UNIQUEIDENTIFIER
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER 
AS BEGIN ATOMIC WITH (
      TRANSACTION ISOLATION LEVEL = SNAPSHOT,
      LANGUAGE = N'English')

	  DECLARE	@myid UNIQUEIDENTIFIER;
	  SELECT @MYID = ID
	  FROM dbo.KeyGuid
	  WHERE [Key] = @key


	SELECT [From], [To], SentDate, Message
	FROM dbo.Messages M
	WHERE (M.[From] = @myid AND M.[To] =  @theirid)
		OR (M.[From] = @theirid AND M.[To] = @myid)
	ORDER BY SentDate DESC
RETURN 0
END