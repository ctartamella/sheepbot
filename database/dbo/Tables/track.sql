CREATE TABLE [dbo].[track] (
    [id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]      NVARCHAR (50) NOT NULL,
    [is_free]   BIT           NOT NULL,
    [is_legacy] BIT           NOT NULL,
    CONSTRAINT [PK_track] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_track_pricing] CHECK (NOT ([is_free]=(1) AND [is_legacy]=(1)))
);
GO


CREATE UNIQUE NONCLUSTERED INDEX [Index_track_name]
    ON [dbo].[track]([name] ASC);
GO

