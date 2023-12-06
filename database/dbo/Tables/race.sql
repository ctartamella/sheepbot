CREATE TABLE [dbo].[race] (
    [id]             BIGINT             IDENTITY (1, 1) NOT NULL,
    [track_id]       BIGINT             NOT NULL,
    [event_id]       BIGINT             NOT NULL,
    [length]         INT                NOT NULL,
    [length_unit_id] INT                NOT NULL,
    [practice_time]  DATETIMEOFFSET (7) NULL,
    [quali_time]     DATETIMEOFFSET (7) NOT NULL,
    [race_type_id]   INT                NOT NULL,
    CONSTRAINT [PK_race] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_race_event] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([id]),
    CONSTRAINT [FK_race_length_unit] FOREIGN KEY ([length_unit_id]) REFERENCES [dbo].[length_unit] ([id]),
    CONSTRAINT [FK_race_track] FOREIGN KEY ([track_id]) REFERENCES [dbo].[track] ([id])
);

