PRINT 'Populating Lookup Table [dbo].[series_type]'

MERGE INTO [dbo].[series_type] as t
    USING (
        VALUES(1,'Pending')
             ,(2,'Active')
             ,(3,'Suspended')
             ,(4,'Deleted')
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