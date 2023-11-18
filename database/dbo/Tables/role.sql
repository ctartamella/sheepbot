CREATE TABLE [dbo].[role] (
    [id]         INT           NOT NULL IDENTITY(1,1),
    [discord_id] BIGINT        NOT NULL,
    [role_name]  NVARCHAR (20) NULL
);
GO

ALTER TABLE [dbo].[role]
    ADD CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED ([id] ASC);
GO

