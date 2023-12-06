
CREATE PROCEDURE dbo.merge_tracks
    @Tracks TrackType   READONLY
AS
BEGIN
    MERGE INTO track t
 USING @Tracks s
    ON s.track_id = t.track_id
WHEN MATCHED THEN UPDATE SET
     t.name = s.name,
     t.is_free = s.is_free,
     t.is_legacy = s.is_legacy,
     t.ai_enabled = s.ai_enabled,
     t.fully_lit = s.fully_lit,
     t.location = s.location,
     t.geo_location = s.geo_location,
     t.price = s.price,
     t.is_purchasable = s.is_purchasable,
     t.is_retired = s.is_retired

WHEN NOT MATCHED BY TARGET THEN
    INSERT (track_id, name, is_free, is_legacy, ai_enabled, fully_lit, location, 
            geo_location, price, is_purchasable, is_retired)
    VALUES (s.track_id, s.name, s.is_free, s.is_legacy, s.ai_enabled, s.fully_lit, s.location, 
            s.geo_location, s.price, s.is_purchasable, s.is_retired)

;
END