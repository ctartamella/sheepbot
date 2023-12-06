CREATE TYPE [dbo].[TrackConfigType] AS TABLE (
    [track_id]        INT            NOT NULL,
    [config_id]       INT            NOT NULL,
    [name]            NVARCHAR (100) NOT NULL,
    [corners]         INT            NOT NULL,
    [length]          FLOAT (53)     NOT NULL,
    [is_dirt]         BIT            NOT NULL,
    [is_oval]         BIT            NOT NULL,
    [max_cars]        TINYINT        NOT NULL,
    [pit_speed_limit] TINYINT        NOT NULL,
    [track_type]      INT            NOT NULL);

