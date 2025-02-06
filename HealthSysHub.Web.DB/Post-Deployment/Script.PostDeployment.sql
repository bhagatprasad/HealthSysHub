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
-- Include seed data for Specialization
:r .\SeedSpecialization.sql

-- Include seed data for Department
:r .\SeedDepartment.sql

-- Include seed data for SeedLabTest
:r .\SeedLabTest.sql

-- Include seed data for SeedPatientType
:r .\SeedPatientType.sql

-- Include seed data for SeedPaymentType
:r .\SeedPaymentType.sql

-- Include seed data for SeedRole
:r .\SeedRole.sql

-- Include seed data for SeedMedicine
:r .\SeedMedicine.sql