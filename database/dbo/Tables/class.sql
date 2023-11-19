CREATE TABLE [dbo].[class] (
    [id]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_class] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [Index_class_name]
    ON [dbo].[class]([name] ASC);
GO

