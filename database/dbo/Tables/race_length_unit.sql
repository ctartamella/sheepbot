CREATE TABLE [dbo].[race_length_unit] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (10) NOT NULL
);
GO

ALTER TABLE [dbo].[race_length_unit]
    ADD CONSTRAINT [PK_race_length_type] PRIMARY KEY CLUSTERED ([id] ASC);
GO

