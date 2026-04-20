-- This adds the missing columns to your Users table
ALTER TABLE AspNetUsers ADD 
    NIC NVARCHAR(MAX) NULL,
    Gender NVARCHAR(MAX) NULL,
    Birthday DATETIME2 NULL,
    Address NVARCHAR(MAX) NULL,
    IndexNumber NVARCHAR(MAX) NULL,
    Batch NVARCHAR(MAX) NULL,
    Faculty NVARCHAR(MAX) NULL,
    Degree NVARCHAR(MAX) NULL;
GO