CREATE TYPE [dbo].[TrackType] AS TABLE (
    [track_id]       INT               NOT NULL,
    [name]           NVARCHAR (50)     NOT NULL,
    [is_free]        BIT               NOT NULL,
    [is_legacy]      BIT               NOT NULL,
    [ai_enabled]     BIT               NOT NULL,
    [fully_lit]      BIT               NOT NULL,
    [location]       NVARCHAR (256)    NULL,
    [geo_location]   [sys].[geography] NOT NULL,
    [price]          NVARCHAR (50)     NULL,
    [is_purchasable] BIT               NOT NULL,
    [is_retired]     BIT               NOT NULL);

