CREATE TABLE [dbo].[RuggerName] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (50)    NULL,
    [Bio]        NVARCHAR (MAX)   NULL,
    [CreateDate] DATETIME2 (7)    DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (DATA_COMPRESSION = PAGE)
);

