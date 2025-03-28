﻿CREATE TABLE [dbo].[HospitalContact]
(
    [HospitalContactId]         UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]                UNIQUEIDENTIFIER NULL,
    [ContactType]               VARCHAR(50) NULL,
    [Phone]                     VARCHAR(20) NULL,
    [Email]                     VARCHAR(100) NULL,
    [CreatedBy]                 UNIQUEIDENTIFIER NULL,
    [CreatedOn]                 DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                UNIQUEIDENTIFIER NULL,
    [ModifiedOn]                DATETIMEOFFSET NULL,
    [IsActive]                  BIT DEFAULT 1
);