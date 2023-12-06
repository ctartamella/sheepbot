CREATE TABLE [dbo].[car] (
    [id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [car_id]    INT            NOT NULL,
    [name]      NVARCHAR (100) NOT NULL,
    [is_free]   BIT            NOT NULL,
    [is_legacy] BIT            NOT NULL,
    CONSTRAINT [PK_cars] PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [CK_car_pricing] CHECK (NOT ([is_free]=(1) AND [is_legacy]=(1)))
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_car_name]
    ON [dbo].[car]([name] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [Index_car_car_id]
    ON [dbo].[car]([car_id] ASC);

