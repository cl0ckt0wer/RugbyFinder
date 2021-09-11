CREATE TABLE [dbo].[Exceptions] (
    [Id]         BIGINT           IDENTITY (1, 1) NOT NULL,
    [SourceGuid] UNIQUEIDENTIFIER NOT NULL,
    [ExString]   NVARCHAR (MAX)   NULL,
    [InsertDate] DATETIME2 (7)    DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_SourceGuid]
    ON [dbo].[Exceptions]([SourceGuid] ASC);

