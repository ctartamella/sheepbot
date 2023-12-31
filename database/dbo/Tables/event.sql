﻿CREATE TABLE [dbo].[event] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [name]           NVARCHAR (100) NOT NULL,
    [role_id]        BIGINT         NOT NULL,
    [discord_server] NVARCHAR (30)  NULL,
    [iracing_url]    NVARCHAR (50)  NULL,
    [website]        NVARCHAR (50)  NULL,
    CONSTRAINT [PK_series] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_series_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[role] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_series_name]
    ON [dbo].[event]([name] ASC);

