CREATE PROCEDURE [dbo].[Proc_UpsertRuggerPic] @key VARCHAR(64)
	,@b VARBINARY(MAX)
AS
DECLARE @u UNIQUEIDENTIFIER;

SELECT @u = Id
FROM KeyGuid
WHERE [Key] = @key;

MERGE dbo.RuggerPic AS target
USING (
	SELECT @u
		,@b
	) AS source(Id, Pic)
	ON (target.Id = source.ID)
WHEN MATCHED
	THEN
		UPDATE
		SET target.Pic = source.Pic
WHEN NOT MATCHED
	THEN
		INSERT (
			Id
			,Pic
			)
		VALUES (
			source.Id
			,source.Pic
			);

RETURN 1
