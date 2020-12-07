CREATE TABLE [dbo].[RuggerLocation] (
    [Id]          UNIQUEIDENTIFIER  NOT NULL,
    [Coordinate]  [sys].[geography] NULL,
    [RefreshDate] DATETIME2 (7)     DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE SPATIAL INDEX [SIndx_RuggerLocation_Coordinate]
    ON [dbo].[RuggerLocation] ([Coordinate]);

