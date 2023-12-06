CREATE TABLE [dbo].[singleton_event] (
    [event_id] BIGINT NOT NULL,
    [race_id]  BIGINT NOT NULL,
    CONSTRAINT [PK_singleton_event] PRIMARY KEY CLUSTERED ([event_id] ASC),
    CONSTRAINT [FK_singleton_event] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([id]),
    CONSTRAINT [FK_singleton_race] FOREIGN KEY ([race_id]) REFERENCES [dbo].[race] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_singleton_event_1]
    ON [dbo].[singleton_event]([race_id] ASC);

