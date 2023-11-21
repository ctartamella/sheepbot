CREATE TABLE [dbo].[race_event_time] (
    [id]            BIGINT NOT NULL,
    [race_id]       BIGINT NOT NULL,
    [event_time_id] BIGINT NULL
);
GO

ALTER TABLE [dbo].[race_event_time]
    ADD CONSTRAINT [FK_race_event_time_race] FOREIGN KEY ([race_id]) REFERENCES [dbo].[race] ([id]);
GO

ALTER TABLE [dbo].[race_event_time]
    ADD CONSTRAINT [FK_race_event_time_event_time] FOREIGN KEY ([event_time_id]) REFERENCES [dbo].[event_time] ([id]);
GO

ALTER TABLE [dbo].[race_event_time]
    ADD CONSTRAINT [PK_race_event_time] PRIMARY KEY CLUSTERED ([id] ASC);
GO

