CREATE TABLE [dbo].[race_class] (
    [id]       BIGINT NOT NULL,
    [race_id]  BIGINT NOT NULL,
    [class_id] BIGINT NOT NULL
);
GO

ALTER TABLE [dbo].[race_class]
    ADD CONSTRAINT [PK_race_car_class] PRIMARY KEY CLUSTERED ([id] ASC);
GO

ALTER TABLE [dbo].[race_class]
    ADD CONSTRAINT [FK_race_car_class_race] FOREIGN KEY ([race_id]) REFERENCES [dbo].[race] ([id]);
GO

ALTER TABLE [dbo].[race_class]
    ADD CONSTRAINT [FK_race_car_class_class] FOREIGN KEY ([class_id]) REFERENCES [dbo].[class] ([id]);
GO

