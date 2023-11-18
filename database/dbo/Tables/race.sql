CREATE TABLE [dbo].[race] (
    [id]             INT                NOT NULL IDENTITY(1,1),
    [series_id]      INT                NULL,
    [track_id]       INT                NOT NULL,
    [practice_time]  DATETIMEOFFSET (7) NULL,
    [quali_time]     DATETIMEOFFSET (7) NOT NULL,
    [length]         INT                NULL,
    [length_type_id] INT                NULL
);
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [CK_race_1] CHECK ([length] IS NOT NULL AND [length_type_id] IS NULL);
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [PK_race] PRIMARY KEY CLUSTERED ([id] ASC);
GO

