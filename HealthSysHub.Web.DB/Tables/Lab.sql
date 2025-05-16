CREATE TABLE [dbo].[Lab]
(
    [LabId]                         UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),  -- Unique identifier for the lab
    [LabName]                       VARCHAR(100)       NULL,                               -- Name of the lab
    [LabCode]                       VARCHAR(100)       NULL,                               -- Code for the lab
    [RegistrationNumber]            VARCHAR(50)        NULL,                               -- Registration number of the lab
    [Address]                      VARCHAR(255)       NULL,                               -- Full address of the lab
    [City]                         VARCHAR(100)       NULL,                               -- City where the lab is located
    [State]                        VARCHAR(100)       NULL,                               -- State where the lab is located
    [Country]                      VARCHAR(100)       NULL,                               -- Country of the lab
    [PostalCode]                   VARCHAR(20)        NULL,                               -- Postal code of the lab
    [PhoneNumber]                  VARCHAR(20)        NULL,                               -- Contact phone number of the lab
    [Email]                       VARCHAR(100)       NULL,                               -- Email address of the lab
    [Website]                     VARCHAR(100)       NULL,                               -- Website URL of the lab
    [LogoUrl]                     VARCHAR(100)       NULL,                               -- Logo URL of the lab
    [HospitalId]                  UNIQUEIDENTIFIER   NULL,                               -- Foreign key reference to the hospital
    [CreatedBy]                  UNIQUEIDENTIFIER   NULL,
    [CreatedOn]                  DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]                DATETIMEOFFSET     NULL,
    [IsActive]                  BIT                DEFAULT 1,
    CONSTRAINT FK_Lab_Hospital FOREIGN KEY (HospitalId) REFERENCES [dbo].[Hospital](HospitalId)
);