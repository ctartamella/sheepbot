CREATE TABLE [dbo].[event_class] (
    [id]       BIGINT IDENTITY (1, 1) NOT NULL,
    [event_id] BIGINT NOT NULL,
    [class_id] BIGINT NOT NULL,
    CONSTRAINT [PK_series_class] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_series_class_class] FOREIGN KEY ([class_id]) REFERENCES [dbo].[class] ([id]),
    CONSTRAINT [FK_series_class_series] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([id])
);


GO
CREATE NONCLUSTERED INDEX [Index_series_class_1]
    ON [dbo].[event_class]([class_id] ASC, [event_id] ASC);

