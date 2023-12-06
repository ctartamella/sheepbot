CREATE TABLE [dbo].[track_config] (
    [id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [track_id]        INT            NOT NULL,
    [config_id]       INT            NOT NULL,
    [name]            NVARCHAR (100) NOT NULL,
    [corners]         INT            NOT NULL,
    [length]          FLOAT (53)     NOT NULL,
    [is_dirt]         BIT            NOT NULL,
    [is_oval]         BIT            NOT NULL,
    [max_cars]        TINYINT        NOT NULL,
    [pit_speed_limit] TINYINT        NOT NULL,
    [track_type]      INT            NOT NULL,
    CONSTRAINT [PK_track_config] PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [FK_track_config_track] FOREIGN KEY ([track_type]) REFERENCES [dbo].[track_type] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_track_config_2]
    ON [dbo].[track_config]([config_id] ASC);


GO
CREATE CLUSTERED INDEX [Index_track_config_1]
    ON [dbo].[track_config]([track_id] ASC);

