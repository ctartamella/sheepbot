CREATE TABLE [dbo].[track_config] (
    [id]       BIGINT         NOT NULL,
    [track_id] INT            NOT NULL,
    [name]     NVARCHAR (100) NOT NULL,
    [corners]  INT            NOT NULL,
    [length]   FLOAT (53)     NOT NULL,
    ALTER TABLE [dbo].[track_config]
    ADD CONSTRAINT [PK_track_config] PRIMARY KEY NONCLUSTERED ([id] ASC);,
    ALTER TABLE [dbo].[track_config]
    ADD CONSTRAINT [FK_track_config_track] FOREIGN KEY ([track_type]) REFERENCES [dbo].[track_type] ([id]);
);


GO
CREATE CLUSTERED INDEX [Index_track_config_1]
    ON [dbo].[track_config]([track_id] ASC);
GO


CREATE UNIQUE NONCLUSTERED INDEX [Index_track_config_2]
    ON [dbo].[track_config]([config_id] ASC);
GO

