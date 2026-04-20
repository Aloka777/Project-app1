-- This script adds the missing columns to your existing Users table
-- It includes safety checks to ensure we don't try to add them if they exist

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'Address')
BEGIN
    ALTER TABLE AspNetUsers ADD 
        Address NVARCHAR(MAX) NULL,
        Birthday DATETIME2 NULL,
        Gender NVARCHAR(MAX) NULL,
        NIC NVARCHAR(MAX) NULL,
        IndexNumber NVARCHAR(MAX) NULL,
        Batch NVARCHAR(MAX) NULL,
        Degree NVARCHAR(MAX) NULL;
END

-- Also check the ProjectProposals table for the new fields we added earlier
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('ProjectProposals') AND name = 'GroupName')
BEGIN
    ALTER TABLE ProjectProposals ADD 
        GroupName NVARCHAR(MAX) NULL,
        TeamMembers NVARCHAR(MAX) NULL,
        TechnicalStack NVARCHAR(MAX) NULL,
        Abstract NVARCHAR(MAX) NULL;
END
