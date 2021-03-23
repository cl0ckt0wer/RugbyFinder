/*
The database must have a MEMORY_OPTIMIZED_DATA filegroup
before the memory optimized object can be created.
*/

CREATE PROCEDURE [dbo].[Proc_InsertRuggerMessage]
	@KEY VARCHAR(64),
	@To UNIQUEIDENTIFIER,
	@Message NVARCHAR(2000)
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER 
AS BEGIN ATOMIC WITH (
      TRANSACTION ISOLATION LEVEL = SNAPSHOT,
      LANGUAGE = N'English')
	  DECLARE 	@From UNIQUEIDENTIFIER
	  SELECT @FROM = ID
	  FROM dbo.KeyGuid
	  WHERE [Key] = @KEY;

	INSERT dbo.Messages ([UId], [From], [To], [Message], [SentDate])
	VALUES (NEWID(), @From, @To, @Message, SYSUTCDATETIME()  )

RETURN 0
END