CREATE TABLE [dbo].[class_car] (
    [id]       INT IDENTITY (1, 1) NOT NULL,
    [class_id] INT NOT NULL,
    [car_id]   INT NOT NULL,
    CONSTRAINT [PK_class_cars] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_class_cars_cars] FOREIGN KEY ([car_id]) REFERENCES [dbo].[car] ([id]),
    CONSTRAINT [FK_class_cars_class] FOREIGN KEY ([car_id]) REFERENCES [dbo].[class] ([id])
);
GO

ALTER TABLE [dbo].[class_car]
    ADD CONSTRAINT [PK_class_cars] PRIMARY KEY CLUSTERED ([id] ASC);
GO

ALTER TABLE [dbo].[class_car]
    ADD CONSTRAINT [FK_class_cars_cars] FOREIGN KEY ([car_id]) REFERENCES [dbo].[car] ([id]);
GO

ALTER TABLE [dbo].[class_car]
    ADD CONSTRAINT [FK_class_cars_class] FOREIGN KEY ([car_id]) REFERENCES [dbo].[class] ([id]);
GO


CREATE UNIQUE NONCLUSTERED INDEX [Index_class_car_pair]
    ON [dbo].[class_car]([car_id] ASC, [class_id] ASC);
GO

