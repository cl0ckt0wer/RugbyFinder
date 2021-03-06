﻿CREATE TABLE [dbo].[RuggerTeam] (
    [Id]       BIGINT           IDENTITY (1, 1) NOT NULL,
    [TeamId]   UNIQUEIDENTIFIER NOT NULL,
    [RuggerId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_RUGGERID]
    ON [dbo].[RuggerTeam]([RuggerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TEAMID]
    ON [dbo].[RuggerTeam]([TeamId] ASC);

