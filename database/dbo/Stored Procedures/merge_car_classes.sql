
CREATE PROCEDURE dbo.merge_car_classes
    @CarClasses CarClassType   READONLY
AS
BEGIN
    MERGE INTO car_class t
 USING @CarClasses s
    ON s.class_id = t.class_id AND s.car_id=t.car_id
WHEN NOT MATCHED BY TARGET THEN
    INSERT (class_id, car_id)
    VALUES (s.class_id, s.car_id)

;
END