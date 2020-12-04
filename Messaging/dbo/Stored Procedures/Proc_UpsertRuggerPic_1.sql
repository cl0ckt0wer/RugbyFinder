CREATE PROCEDURE [dbo].[Proc_UpsertRuggerPic]
	@u UNIQUEIDENTIFIER,
	@b VARBINARY(MAX)
AS
	
	    MERGE dbo.RuggerPic AS target  
    USING (SELECT @u, @b) AS source (Id, Pic)  
    ON (target.Id = source.ID)  
    WHEN MATCHED THEN
        UPDATE SET target.Pic = source.Pic
    WHEN NOT MATCHED THEN  
        INSERT (Id, Pic)  
        VALUES (source.Id, source.Pic);

RETURN 1