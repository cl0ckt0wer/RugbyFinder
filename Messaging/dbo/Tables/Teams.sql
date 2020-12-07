CREATE TABLE [dbo].[Teams] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [TeamName]   NVARCHAR (200)   NULL,
    [TeamCityId] INT              NULL,
    [TeamBio]    NVARCHAR (MAX)   NULL,
    [TeamOwner]  UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TeamOwner]
    ON [dbo].[Teams]([TeamOwner] ASC);

