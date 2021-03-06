﻿CREATE TABLE [dbo].[KeyGuid]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT = 131072),
	[Key] VARCHAR(64) NOT NULL UNIQUE,
	[InsertDate] DATETIME NOT NULL DEFAULT(GETDATE())
) WITH (MEMORY_OPTIMIZED = ON)
