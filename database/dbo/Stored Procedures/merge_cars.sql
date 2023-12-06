
CREATE PROCEDURE dbo.merge_cars
    @Cars CarType   READONLY
AS
BEGIN
    MERGE INTO car t
 USING @Cars s
    ON s.car_id = t.car_id
WHEN MATCHED THEN UPDATE SET
     t.name = s.name,
     t.is_free = s.is_free,
     t.is_legacy = s.is_legacy

WHEN NOT MATCHED BY TARGET THEN
    INSERT (car_id, name, is_free, is_legacy)
    VALUES (s.car_id, s.name, s.is_free, s.is_legacy)

;
END