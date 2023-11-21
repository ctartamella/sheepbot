CREATE TABLE [dbo].[race_series] (
    [id]        BIGINT NOT NULL,
    [race_id]   BIGINT NOT NULL,
    [series_id] BIGINT NOT NULL
);
GO

ALTER TABLE [dbo].[race_series]
    ADD CONSTRAINT [FK_race_series_race] FOREIGN KEY ([race_id]) REFERENCES [dbo].[race] ([id]);
GO

ALTER TABLE [dbo].[race_series]
    ADD CONSTRAINT [FK_race_series_series] FOREIGN KEY ([series_id]) REFERENCES [dbo].[series] ([id]);
GO

ALTER TABLE [dbo].[race_series]
    ADD CONSTRAINT [PK_race_series] PRIMARY KEY CLUSTERED ([id] ASC);
GO

