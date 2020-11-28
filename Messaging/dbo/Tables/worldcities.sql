CREATE TABLE [dbo].[worldcities] (
    [city]       NVARCHAR (2000) NOT NULL,
    [city_ascii] NVARCHAR (2000) NOT NULL,
    [lat]        FLOAT (53)      NOT NULL,
    [lng]        FLOAT (53)      NOT NULL,
    [country]    NVARCHAR (2000) NOT NULL,
    [iso2]       NVARCHAR (2000) NOT NULL,
    [iso3]       NVARCHAR (2000) NOT NULL,
    [admin_name] NVARCHAR (2000) NULL,
    [capital]    NVARCHAR (2000) NULL,
    [population] NVARCHAR (2000) NULL,
    [id]         INT             NOT NULL,
    CONSTRAINT [PK_worldcities] PRIMARY KEY CLUSTERED ([id] ASC)
);

