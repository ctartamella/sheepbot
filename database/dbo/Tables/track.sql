CREATE TABLE [dbo].[track] (
    [id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]      NVARCHAR (50) NOT NULL,
    [is_free]   BIT           NOT NULL,
    [is_legacy] BIT           NOT NULL,
    ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [PK_track] PRIMARY KEY NONCLUSTERED ([id] ASC);,
    CONSTRAINT [CK_track_pricing] CHECK (NOT ([is_free]=(1) AND [is_legacy]=(1)))
);
GO


CREATE UNIQUE NONCLUSTERED INDEX [Index_track_name]
    ON [dbo].[track]([name] ASC);
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_ai_enabled] DEFAULT ((0)) FOR [ai_enabled];
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_fully_lit] DEFAULT ((0)) FOR [fully_lit];
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_is_dirt] DEFAULT ((0)) FOR [is_dirt];
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_is_legacy] DEFAULT ((0)) FOR [is_legacy];
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_is_oval] DEFAULT ((0)) FOR [is_oval];
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_is_purchasable] DEFAULT ((1)) FOR [is_purchasable];
GO


ALTER TABLE [dbo].[track]
    ADD CONSTRAINT [DEFAULT_track_is_retired] DEFAULT ((0)) FOR [is_retired];
GO


CREATE UNIQUE CLUSTERED INDEX [Index_iracing_id]
    ON [dbo].[track]([iracing_id] ASC);
GO

