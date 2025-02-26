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

-- Include seed data for Lab Test
:r .\SeedLabTest.sql

-- Include seed data for Patient Type
:r .\SeedPatientType.sql

-- Include seed data for Payment Type
:r .\SeedPaymentType.sql

-- Include seed data for Role
:r .\SeedRole.sql

-- Include seed data for Medicine
:r .\SeedMedicine.sql

-- Include seed data for Hospital Type
:r .\SeedHospitalType.sql