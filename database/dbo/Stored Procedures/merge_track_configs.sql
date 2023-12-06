
CREATE PROCEDURE dbo.merge_track_configs
    @Configs TrackConfigType   READONLY
AS
BEGIN
    MERGE INTO track_config t
 USING @Configs s
    ON s.track_id = t.track_id AND s.config_id = t.config_id
WHEN MATCHED THEN UPDATE SET
     t.name = s.name,
     t.corners = s.corners,
     t.length = s.length,
     t.is_dirt = s.is_dirt,
     t.is_oval = s.is_oval,
     t.max_cars = s.max_cars,
     t.pit_speed_limit = s.pit_speed_limit,
     t.track_type = s.track_type

WHEN NOT MATCHED BY TARGET THEN
    INSERT (track_id, config_id, name, corners, length, is_dirt, is_oval, max_cars, pit_speed_limit, track_type)
    VALUES (s.track_id, s.config_id, s.name, s.corners, s.length, s.is_dirt, s.is_oval, s.max_cars, s.pit_speed_limit, s.track_type)

;
END