CREATE TABLE [dbo].[series] (
    [id]             INT            NOT NULL IDENTITY(1,1),
    [name]           NVARCHAR (100) NOT NULL,
    [role_id]        INT            NOT NULL,
    [series_type_id] INT            NOT NULL,
    [discord_server] NVARCHAR (30)  NULL,
    [iracing_url]    NVARCHAR (50)  NULL,
    [website]        NVARCHAR (50)  NULL
);
GO

ALTER TABLE [dbo].[series]
    ADD CONSTRAINT [FK_series_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[role] ([id]);
GO

ALTER TABLE [dbo].[series]
    ADD CONSTRAINT [FK_series_series_type] FOREIGN KEY ([series_type_id]) REFERENCES [dbo].[series_type] ([id]);
GO

ALTER TABLE [dbo].[series]
    ADD CONSTRAINT [PK_series] PRIMARY KEY CLUSTERED ([id] ASC);
GO

