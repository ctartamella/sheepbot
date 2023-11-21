CREATE TABLE [dbo].[series_event_length] (
    [id]              BIGINT NOT NULL,
    [series_id]       BIGINT NOT NULL,
    [event_length_id] BIGINT NULL
);
GO

ALTER TABLE [dbo].[series_event_length]
    ADD CONSTRAINT [FK_series_event_length_series] FOREIGN KEY ([series_id]) REFERENCES [dbo].[series] ([id]);
GO

ALTER TABLE [dbo].[series_event_length]
    ADD CONSTRAINT [FK_series_event_length_event_length] FOREIGN KEY ([event_length_id]) REFERENCES [dbo].[event_length] ([id]);
GO

ALTER TABLE [dbo].[series_event_length]
    ADD CONSTRAINT [PK_series_event_length] PRIMARY KEY CLUSTERED ([id] ASC);
GO

