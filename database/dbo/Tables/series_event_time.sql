CREATE TABLE [dbo].[series_event_time] (
    [id]            BIGINT NOT NULL,
    [series_id]     BIGINT NOT NULL,
    [event_time_id] BIGINT NOT NULL
);
GO

ALTER TABLE [dbo].[series_event_time]
    ADD CONSTRAINT [PK_series_event_time] PRIMARY KEY CLUSTERED ([id] ASC);
GO

ALTER TABLE [dbo].[series_event_time]
    ADD CONSTRAINT [FK_series_event_time_event_time] FOREIGN KEY ([event_time_id]) REFERENCES [dbo].[event_time] ([id]);
GO

ALTER TABLE [dbo].[series_event_time]
    ADD CONSTRAINT [FK_series_event_time_series] FOREIGN KEY ([series_id]) REFERENCES [dbo].[series] ([id]);
GO

