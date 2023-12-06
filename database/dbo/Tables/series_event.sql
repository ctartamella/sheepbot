CREATE TABLE [dbo].[series_event] (
    [event_id] BIGINT NOT NULL,
    [race_id]  BIGINT NOT NULL,
    CONSTRAINT [PK_series_event] PRIMARY KEY CLUSTERED ([event_id] ASC, [race_id] ASC),
    CONSTRAINT [FK_series_event_event] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([id]),
    CONSTRAINT [FK_series_event_race] FOREIGN KEY ([race_id]) REFERENCES [dbo].[race] ([id])
);


GO
CREATE NONCLUSTERED INDEX [Index_series_event_1]
    ON [dbo].[series_event]([race_id] ASC);

