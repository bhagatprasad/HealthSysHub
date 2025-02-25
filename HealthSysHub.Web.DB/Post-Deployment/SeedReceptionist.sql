/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO [dbo].[Receptionist] AS target
USING (
    SELECT 
        StaffId,
        HospitalId,
        CONCAT(FirstName, ' ', LastName) AS FullName,
        NULL AS Description,  -- You can modify this if you have a description
        NULL AS Experience,    -- You can modify this if you have experience data
        NULL AS Education,     -- You can modify this if you have education data
        NULL AS Awards,        -- You can modify this if you have awards data
        NULL AS ProfileUrl,    -- You can modify this if you have a profile URL
        Phone AS PhoneNumber,
        Email,
        NULL AS Address,       -- You can modify this if you have an address
        NULL AS DateOfBirth,   -- You can modify this if you have a date of birth
        NULL AS Gender,        -- You can modify this if you have gender data
        SYSDATETIMEOFFSET() AS JoiningDate,  -- Assuming joining date is now
        'Active' AS Status,    -- You can modify this if you have a different status
        NULL AS CreatedBy,
        SYSDATETIMEOFFSET() AS CreatedOn,
        NULL AS ModifiedBy,
        NULL AS ModifiedOn,
        1 AS IsActive
    FROM [dbo].[HospitalStaff]
    WHERE Designation = 'Receptionist'
) AS source
ON target.StaffId = source.StaffId  
WHEN NOT MATCHED THEN
    INSERT (
        HospitalId,
        StaffId,
        FullName,
        Description,
        Experience,
        Education,
        ProfileUrl,
        PhoneNumber,
        Email,
        Address,
        DateOfBirth,
        Gender,
        JoiningDate,
        Status,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES (
        source.HospitalId,
        source.StaffId,
        source.FullName,
        source.Description,
        source.Experience,
        source.Education,
        source.ProfileUrl,
        source.PhoneNumber,
        source.Email,
        source.Address,
        source.DateOfBirth,
        source.Gender,
        source.JoiningDate,
        source.Status,
        source.CreatedBy,
        source.CreatedOn,
        source.ModifiedBy,
        source.ModifiedOn,
        source.IsActive
    );