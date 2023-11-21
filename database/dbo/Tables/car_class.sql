CREATE TABLE [dbo].[car_class] (
    [id]       BIGINT IDENTITY (1, 1) NOT NULL,
    [class_id] BIGINT NOT NULL,
    [car_id]   BIGINT NOT NULL
);
GO

ALTER TABLE [dbo].[car_class]
    ADD CONSTRAINT [PK_class_cars] PRIMARY KEY CLUSTERED ([id] ASC);
GO

ALTER TABLE [dbo].[car_class]
    ADD CONSTRAINT [FK_class_cars_class] FOREIGN KEY ([class_id]) REFERENCES [dbo].[class] ([id]);
GO

ALTER TABLE [dbo].[car_class]
    ADD CONSTRAINT [FK_class_cars_cars] FOREIGN KEY ([car_id]) REFERENCES [dbo].[car] ([id]);
GO

CREATE UNIQUE NONCLUSTERED INDEX [Index_class_car_pair]
    ON [dbo].[car_class]([car_id] ASC, [class_id] ASC);
GO

