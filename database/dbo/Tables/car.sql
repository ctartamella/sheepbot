CREATE TABLE [dbo].[car] (
    [id]        INT           NOT NULL IDENTITY(1,1),
    [name]      NVARCHAR (50) NOT NULL,
    [is_free]   BIT           NOT NULL,
    [is_legacy] BIT           NOT NULL
);
GO

ALTER TABLE [dbo].[car]
    ADD CONSTRAINT [CK_car_pricing] CHECK (NOT ([is_free]=1 AND [is_legacy]=1));
GO

ALTER TABLE [dbo].[car]
    ADD CONSTRAINT [PK_cars] PRIMARY KEY CLUSTERED ([id] ASC);
GO

