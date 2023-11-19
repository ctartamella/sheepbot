CREATE TABLE [dbo].[series_class] (
    [id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [series_id] BIGINT NOT NULL,
    [class_id]  BIGINT NOT NULL
);
GO

ALTER TABLE [dbo].[series_class]
    ADD CONSTRAINT [FK_series_class_class] FOREIGN KEY ([class_id]) REFERENCES [dbo].[class] ([id]);
GO

ALTER TABLE [dbo].[series_class]
    ADD CONSTRAINT [FK_series_class_series] FOREIGN KEY ([series_id]) REFERENCES [dbo].[series] ([id]);
GO

CREATE NONCLUSTERED INDEX [Index_series_class_1]
    ON [dbo].[series_class]([class_id] ASC, [series_id] ASC);
GO

ALTER TABLE [dbo].[series_class]
    ADD CONSTRAINT [PK_series_class] PRIMARY KEY CLUSTERED ([id] ASC);
GO

