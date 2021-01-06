CREATE TABLE [dbo].[Exceptions]
(
	[Id] BIGINT IDENTITY PRIMARY KEY,
	[SourceGuid] UNIQUEIDENTIFIER NOT NULL,
	[ExString] NVARCHAR(MAX)
	INDEX IX_SourceGuid (SourceGuid)
)
