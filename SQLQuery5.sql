-- This ensures the student table has all the slots needed for the dashboard
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'Address')
BEGIN
    ALTER TABLE AspNetUsers ADD 
        NIC NVARCHAR(MAX) NULL,
        Gender NVARCHAR(MAX) NULL,
        Birthday DATETIME2 NULL,
        Address NVARCHAR(MAX) NULL,
        Batch NVARCHAR(MAX) NULL,
        Degree NVARCHAR(MAX) NULL;
END