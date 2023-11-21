CREATE TABLE [dbo].[event_time] (
    [id]            BIGINT             NOT NULL,
    [practice_time] DATETIMEOFFSET (7) NULL,
    [quali_time]    DATETIMEOFFSET (7) NOT NULL
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [Index_race_time_times]
    ON [dbo].[event_time]([practice_time] DESC, [quali_time] DESC);
GO

ALTER TABLE [dbo].[event_time]
    ADD CONSTRAINT [PK_race_time] PRIMARY KEY CLUSTERED ([id] ASC);
GO

