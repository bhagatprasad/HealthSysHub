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
MERGE INTO [dbo].[HospitalType] AS target
USING (
    VALUES 
        ('Super Specialty', 'Hospitals that focus on specific areas of medicine, offering advanced treatments and specialized care.'),
        ('Multi Specialty', 'Hospitals that provide a range of services across various medical specialties, catering to diverse health needs.'),
        ('Community Health Center', 'Local clinics that offer primary and preventive care, often serving underserved populations.'),
        ('Primary Health Center', 'Facilities that focus on basic health care services, emphasizing preventive care and health education.'),
        ('Area Hospital', 'Hospitals that serve a specific geographic area, providing a mix of emergency and general medical services.')
) AS source (HospitalTypeName, Description)
ON target.HospitalTypeName = source.HospitalTypeName
WHEN NOT MATCHED THEN
    INSERT (HospitalTypeName, Description, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
    VALUES (source.HospitalTypeName, source.Description, NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.Description = source.Description,
        target.ModifiedBy = NULL,
        target.ModifiedOn = SYSDATETIMEOFFSET(),
        target.IsActive = 1;