CREATE TABLE [dbo].[race] (
    [id]             INT                IDENTITY (1, 1) NOT NULL,
    [series_id]      INT                NULL,
    [track_id]       INT                NOT NULL,
    [practice_time]  DATETIMEOFFSET (7) NULL,
    [quali_time]     DATETIMEOFFSET (7) NOT NULL,
    [length]         INT                NULL,
    [length_type_id] INT                NULL,
    CONSTRAINT [PK_race] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_race_1] CHECK ([length] IS NULL OR [length] IS NOT NULL AND [length_type_id] IS NOT NULL)
);
GO

