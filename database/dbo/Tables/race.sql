CREATE TABLE [dbo].[race] (
    [id]             BIGINT             IDENTITY (1, 1) NOT NULL,
    [series_id]      BIGINT             NULL,
    [track_id]       BIGINT             NOT NULL,
    [practice_time]  DATETIMEOFFSET (7) NULL,
    [quali_time]     DATETIMEOFFSET (7) NOT NULL,
    [length]         INT                NULL,
    [length_unit_id] INT                NULL
);
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [FK_race_track] FOREIGN KEY ([track_id]) REFERENCES [dbo].[track] ([id]);
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [FK_race_length_type] FOREIGN KEY ([length_unit_id]) REFERENCES [dbo].[race_length_unit] ([id]);
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [FK_race_series] FOREIGN KEY ([series_id]) REFERENCES [dbo].[series] ([id]);
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [CK_race_length_unit] CHECK ([length] IS NULL OR [length] IS NOT NULL AND [length_unit_id] IS NOT NULL);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If length is defined, enforce unit being set.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'race', @level2type = N'CONSTRAINT', @level2name = N'CK_race_length_unit';
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [CK_race_session_times] CHECK ([practice_time]<[quali_time]);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ensure practice time occurs before quali', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'race', @level2type = N'CONSTRAINT', @level2name = N'CK_race_session_times';
GO

ALTER TABLE [dbo].[race]
    ADD CONSTRAINT [PK_race] PRIMARY KEY CLUSTERED ([id] ASC);
GO

