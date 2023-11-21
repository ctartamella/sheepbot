CREATE TABLE [dbo].[race] (
    [id]       BIGINT IDENTITY (1, 1) NOT NULL,
    [track_id] BIGINT NOT NULL,
    CONSTRAINT [PK_race] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_race_track] FOREIGN KEY ([track_id]) REFERENCES [dbo].[track] ([id])
);
GO

