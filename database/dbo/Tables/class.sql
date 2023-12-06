CREATE TABLE [dbo].[class] (
    [id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [class_id] INT           NOT NULL,
    [name]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_class] PRIMARY KEY NONCLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_class_name]
    ON [dbo].[class]([name] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [Index_class_class_id]
    ON [dbo].[class]([class_id] ASC);

