CREATE PROCEDURE [dbo].[Proc_UpsertTeam]
	@id uniqueidentifier,
    @name nvarchar(200),
    @bio nvarchar(max),
    @cityid int,
    @owner uniqueidentifier,
    @teampic varbinary(max)
AS
	    MERGE dbo.Teams AS target  
    USING (SELECT @id, @name, @bio, @cityid, @owner) AS source (Id, Name, Bio, CityId, Owner)  
    ON (target.Id = source.ID)  
    WHEN MATCHED THEN
        UPDATE SET TeamName = source.Name, TeamBio = source.Bio, TeamCityId = CityId, TeamOwner = Owner
    WHEN NOT MATCHED THEN  
        INSERT (Id, TeamName, TeamBio, TeamCityId, TeamOwner)  
        VALUES (source.Id, source.Name, source.Bio, source.CityId, Source.Owner);

          MERGE dbo.TeamPic target  
    USING (SELECT @id, @teampic) AS source (Id, TeamPic)  
    ON (target.Id = source.ID)  
    WHEN MATCHED THEN 
        UPDATE SET target.Pic = source.TeamPic
    WHEN NOT MATCHED THEN  
        INSERT (Id, Pic)  
        VALUES (source.Id, source.TeamPic);

RETURN 1