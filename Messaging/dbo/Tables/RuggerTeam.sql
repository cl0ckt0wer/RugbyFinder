﻿CREATE TABLE [dbo].[RuggerTeam]
(
	[Id] BIGINT NOT NULL IDENTITY PRIMARY KEY,
	[TeamId] UNIQUEIDENTIFIER NOT NULL ,
	[RuggerId] UNIQUEIDENTIFIER NOT NULL
	INDEX IX_TEAMID ([TeamId]),
	INDEX IX_RUGGERID ([RuggerId])

)
