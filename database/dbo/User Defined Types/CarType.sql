CREATE TYPE [dbo].[CarType] AS TABLE (
    [car_id]    INT            NOT NULL,
    [name]      NVARCHAR (100) NOT NULL,
    [is_free]   BIT            NOT NULL,
    [is_legacy] BIT            NOT NULL);

