CREATE TABLE [dbo].[role] (
    [id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [discord_id] BIGINT        NOT NULL,
    [role_name]  NVARCHAR (20) NULL,
    CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_role_name]
    ON [dbo].[role]([role_name] ASC);

