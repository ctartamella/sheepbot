CREATE TABLE [dbo].[class_car] (
    [id]       BIGINT IDENTITY (1, 1) NOT NULL,
    [class_id] BIGINT NOT NULL,
    [car_id]   BIGINT NOT NULL,
    CONSTRAINT [PK_class_cars] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_class_cars_cars] FOREIGN KEY ([car_id]) REFERENCES [dbo].[car] ([id]),
    CONSTRAINT [FK_class_cars_class] FOREIGN KEY ([class_id]) REFERENCES [dbo].[class] ([id])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [Index_class_car_pair]
    ON [dbo].[class_car]([car_id] ASC, [class_id] ASC);
GO


