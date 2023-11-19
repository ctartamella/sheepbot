CREATE TABLE [dbo].[track] (
    [id]        BIGINT        NOT NULL IDENTITY(1,1),
    [name]      NVARCHAR (50) NOT NULL,
    [is_free]   BIT           NOT NULL,
    [is_legacy] BIT           NOT NULL
);
GO

ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [CK_track_pricing] CHECK (NOT ([is_free]=1 AND [is_legacy]=1));
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [PK_track] PRIMARY KEY CLUSTERED ([id] ASC);
GO

