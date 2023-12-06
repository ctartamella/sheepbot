CREATE TABLE [dbo].[car_class] (
    [id]       BIGINT IDENTITY (1, 1) NOT NULL,
    [class_id] INT    NOT NULL,
    [car_id]   INT    NOT NULL,
    CONSTRAINT [PK_class_cars] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_car_class_car] FOREIGN KEY ([car_id]) REFERENCES [dbo].[car] ([car_id]),
    CONSTRAINT [FK_car_class_class] FOREIGN KEY ([class_id]) REFERENCES [dbo].[class] ([class_id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_class_car_pair]
    ON [dbo].[car_class]([car_id] ASC, [class_id] ASC);

