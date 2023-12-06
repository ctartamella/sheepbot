
CREATE PROCEDURE dbo.merge_classes
    @Classes ClassType   READONLY
AS
BEGIN
    MERGE INTO class t
 USING @Classes s
    ON s.class_id = t.class_id
WHEN MATCHED THEN UPDATE SET
     t.name = s.name

WHEN NOT MATCHED BY TARGET THEN
    INSERT (class_id, name)
    VALUES (s.class_id, s.name)

;
END