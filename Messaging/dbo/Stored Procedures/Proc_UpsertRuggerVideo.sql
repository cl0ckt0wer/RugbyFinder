CREATE PROCEDURE [dbo].[Proc_UpsertRuggerVideo] @key VARCHAR(64)
	,@filename VARCHAR(255)
AS
DECLARE @ID UNIQUEIDENTIFIER;

SELECT @ID = Id
FROM KeyGuid
WHERE [Key] = @key;

MERGE dbo.RuggerVideo AS target
USING (
	SELECT @id
		,@filename
	) AS source(Id, FileName)
	ON (target.Id = source.ID)
WHEN MATCHED
	THEN
		UPDATE
		SET target.FileName = source.FileName
WHEN NOT MATCHED
	THEN
		INSERT (
			Id
			,FileName
			)
		VALUES (
			source.Id
			,source.FileName
			);

RETURN 0
