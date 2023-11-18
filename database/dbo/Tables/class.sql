CREATE TABLE [dbo].[class] (
    [id]          INT           NOT NULL IDENTITY(1,1),
    [name]        NVARCHAR (50) NOT NULL
);
GO

ALTER TABLE [dbo].[class]
    ADD CONSTRAINT [PK_class] PRIMARY KEY CLUSTERED ([id] ASC);
GO

