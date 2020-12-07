﻿CREATE TABLE [dbo].[RuggerPic] (
    [Id]  UNIQUEIDENTIFIER           ROWGUIDCOL NOT NULL,
    [Pic] VARBINARY (MAX) FILESTREAM NULL,
    UNIQUE NONCLUSTERED ([Id] ASC)
) FILESTREAM_ON [FS1];

