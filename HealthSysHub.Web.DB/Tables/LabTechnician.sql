﻿CREATE TABLE [dbo].[LabTechnician]
(
    [LabTechnicianId]  UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]       UNIQUEIDENTIFIER NULL,
    [StaffId]          UNIQUEIDENTIFIER  NULL,
    [FullName]         VARCHAR(100) NULL,
    [Description]      VARCHAR(MAX) NULL,
    [Experience]       VARCHAR(100) NULL,
    [Education]        VARCHAR(MAX) NULL,
    [ProfileUrl]       VARCHAR(255) NULL,
    [PhoneNumber]      VARCHAR(20) NULL,
    [Email]            VARCHAR(100) NULL,
    [Address]          VARCHAR(255) NULL,
    [DateOfBirth]      DATETIMEOFFSET NULL,
    [Gender]           VARCHAR(10) NULL,
    [JoiningDate]      DATETIMEOFFSET NULL,
    [Status]           VARCHAR(50) NULL,
    [CreatedBy]        UNIQUEIDENTIFIER NULL,
    [CreatedOn]        DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]       UNIQUEIDENTIFIER NULL,
    [ModifiedOn]       DATETIMEOFFSET NULL,
    [IsActive]         BIT DEFAULT 1,
    CONSTRAINT FK_LabTechnician_Hospital FOREIGN KEY (HospitalId) REFERENCES [dbo].[Hospital](HospitalId)
);