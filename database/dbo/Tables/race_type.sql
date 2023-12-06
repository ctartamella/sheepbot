CREATE TABLE [dbo].[race_type] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (10) NOT NULL
);
GO

ALTER TABLE [dbo].[race_type]
    ADD CONSTRAINT [PK_series_type] PRIMARY KEY CLUSTERED ([id] ASC);
GO

