PRINT 'Populating Lookup Table [dbo].[race_length_unit]'

MERGE INTO [dbo].[race_length_unit] as t
    USING (
        VALUES(1,'Minutes')
             ,(2,'Hours')
             ,(3,'Laps')
             ,(4,'Miles')
             ,(5,'Kilometers')
    ) s ([id],[name])
        ON t.[id] = s.[id]
WHEN MATCHED
    THEN 
        UPDATE
            SET [name] = s.[name]
WHEN NOT MATCHED BY TARGET
    AND [s].[id] IS NOT NULL 
    THEN
        INSERT 
        (
            [id]
            ,[name]
        )
        VALUES 
        (
            [id]
            ,[name]
        )
WHEN NOT MATCHED BY SOURCE 
   THEN 
       DELETE;

GO