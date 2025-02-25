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
MERGE INTO [dbo].[User ] AS target
USING (
    SELECT 
        StaffId,
        HospitalId,
        FirstName,
        LastName,
        Email,
        Phone,
        'U5iyj7/ux+KH+HC8JtHuVrc1i2G4Oe7THogkhh3KWdN+Z3T4IbHMcF1tR7/mzdY5ucIdE7o67jONfwmBgjCZDk5AAZjEss30Qp7niHDT1ZDeKaNMUqjjb8e0strmqlSwNzfOFheyiCcXAdHrAjetHiQ2YZ1x5d3bXD/tURDKbobXM1PeSiPj4KZ/l9zPh+Jw+PDddDrCneSSOAYC5nzo6rc/Zys+yPd4E1ewhtjxGfvlJpDtZ1SyyNGfI4IYrtWFnBQ/uNxzO1zIa8IwCU/ZjwsdevRSPmV4VeocF/ahF1rQh6/oUl7w/rPukiLmtDt6ckMYXKuXlda9CP7+B2Bx+g==' AS PasswordHash,
        'SxP3PHzL5yHJS9GOGeG6pUFjibxDHi7bcfqJAsjpe5Qu7QfUdx7w7FuKrYlMYGOUBsNrKyg1E8N9DKlz4kYXfg==' AS PasswordSalt,
        RoleId,
        NULL AS LastPasswordChangedOn,
        NULL AS CreatedBy,
        SYSDATETIMEOFFSET() AS CreatedOn,
        NULL AS ModifiedBy,
        NULL AS ModifiedOn,
        1 AS IsActive
    FROM [dbo].[HospitalStaff]
) AS source
ON target.StaffId = source.StaffId 
WHEN NOT MATCHED THEN
    INSERT (
        HospitalId,
        FirstName,
        LastName,
        Email,
        Phone,
        PasswordHash,
        PasswordSalt,
        RoleId,
        StaffId,
        LastPasswordChangedOn,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES (
        source.HospitalId,
        source.FirstName,
        source.LastName,
        source.Email,
        source.Phone,
        source.PasswordHash,
        source.PasswordSalt,
        source.RoleId, 
        source.StaffId,
        source.LastPasswordChangedOn,
        source.CreatedBy,
        source.CreatedOn,
        source.ModifiedBy,
        source.ModifiedOn,
        source.IsActive
    )
WHEN MATCHED THEN
    UPDATE SET
        target.HospitalId = source.HospitalId,
        target.FirstName = source.FirstName,
        target.LastName = source.LastName,
        target.Email = source.Email,
        target.Phone = source.Phone,
        target.PasswordHash = source.PasswordHash,
        target.PasswordSalt = source.PasswordSalt,
        target.RoleId = source.RoleId,
        target.LastPasswordChangedOn = source.LastPasswordChangedOn,
        target.CreatedBy = source.CreatedBy,
        target.CreatedOn = source.CreatedOn,
        target.ModifiedBy = source.ModifiedBy,
        target.ModifiedOn = source.ModifiedOn,
        target.IsActive = source.IsActive;