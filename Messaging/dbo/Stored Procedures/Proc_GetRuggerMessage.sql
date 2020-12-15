/*
The database must have a MEMORY_OPTIMIZED_DATA filegroup
before the memory optimized object can be created.
*/

CREATE PROCEDURE [dbo].[Proc_GetRuggerMessage]
	@myid UNIQUEIDENTIFIER,
	@theirid UNIQUEIDENTIFIER
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER 
AS BEGIN ATOMIC WITH (
      TRANSACTION ISOLATION LEVEL = SNAPSHOT,
      LANGUAGE = N'English')
	SELECT [From], [To], SentDate, Message
	FROM dbo.Messages M
	WHERE (M.[From] = @myid AND M.[To] =  @theirid)
		OR (M.[From] = @theirid AND M.[To] = @myid)
	ORDER BY SentDate DESC
RETURN 0
END