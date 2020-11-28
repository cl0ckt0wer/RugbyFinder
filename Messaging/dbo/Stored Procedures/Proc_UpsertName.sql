create procedure Proc_UpsertName  @id uniqueidentifier, @name nvarchar(50)
as 
    MERGE dbo.RuggerName AS target  
    USING (SELECT @id, @name) AS source (Id, Name)  
    ON (target.Id = source.ID)  
    WHEN MATCHED THEN
        UPDATE SET Name = source.Name  
    WHEN NOT MATCHED THEN  
        INSERT (Id, Name)  
        VALUES (source.Id, source.Name);