CREATE PROC Proc_UpsertName  @key varchar(64), @name nvarchar(50), @bio nvarchar(max)
AS 
    DECLARE @ID UNIQUEIDENTIFIER;
    SELECT @ID = Id
    FROM KeyGuid
    WHERE [Key] = @key;

    MERGE dbo.RuggerName AS target  
    USING (SELECT @id, @name, @bio) AS source (Id, Name, Bio)  
    ON (target.Id = source.ID)  
    WHEN MATCHED THEN
        UPDATE SET Name = source.Name, Bio = source.Bio
    WHEN NOT MATCHED THEN  
        INSERT (Id, Name, Bio)  
        VALUES (source.Id, source.Name, source.Bio);