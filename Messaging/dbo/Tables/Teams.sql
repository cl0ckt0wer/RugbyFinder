﻿CREATE TABLE [dbo].[Teams]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[TeamName] NVARCHAR(200),
	[TeamCityId] INT,
	[TeamBio] NVARCHAR(MAX), 
    [TeamOwner] UNIQUEIDENTIFIER NOT NULL
	INDEX IX_TeamOwner ([TeamOwner])
	
)
