CREATE TABLE [dbo].[race_event_length] (
    [id]                   BIGINT NOT NULL,
    [race_id]              BIGINT NOT NULL,
    [race_event_length_id] BIGINT NOT NULL
);
GO

ALTER TABLE [dbo].[race_event_length]
    ADD CONSTRAINT [FK_race_event_length_race_event_length] FOREIGN KEY ([race_event_length_id]) REFERENCES [dbo].[race_event_length] ([id]);
GO

ALTER TABLE [dbo].[race_event_length]
    ADD CONSTRAINT [FK_race_event_length_race] FOREIGN KEY ([race_id]) REFERENCES [dbo].[race] ([id]);
GO

ALTER TABLE [dbo].[race_event_length]
    ADD CONSTRAINT [PK_race_event_length] PRIMARY KEY CLUSTERED ([id] ASC);
GO

