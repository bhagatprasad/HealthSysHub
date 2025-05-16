CREATE TABLE [dbo].[Pharmacy]
(
    [PharmacyId]                     UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),  -- Unique identifier for the pharmacy
    [PharmacyName]                   VARCHAR(100)       NULL,                               -- Name of the pharmacy
    [PharmacyCode]                   VARCHAR(100)       NULL,                               -- Code of the pharmacy
    [RegistrationNumber]             VARCHAR(50)        NULL,                               -- Registration number of the pharmacy
    [Address]                        VARCHAR(255)       NULL,                               -- Full address of the pharmacy
    [City]                           VARCHAR(100)        NULL,                               -- City where the pharmacy is located
    [State]                          VARCHAR(100)        NULL,                               -- State where the pharmacy is located
    [Country]                        VARCHAR(100)        NULL,                               -- Country of the pharmacy
    [PostalCode]                     VARCHAR(20)         NULL,                               -- Postal code of the pharmacy
    [PhoneNumber]                    VARCHAR(20)         NULL,                               -- Contact phone number of the pharmacy
    [Email]                          VARCHAR(100)        NULL,                               -- Email address of the pharmacy
    [Website]                        VARCHAR(100)        NULL,                               -- Website URL of the pharmacy
    [LogoUrl]                        VARCHAR(100)        NULL,                               -- Logo URL of the pharmacy
    [HospitalId]                     UNIQUEIDENTIFIER    NULL,                               -- Foreign key reference to the hospital
    [CreatedBy]                      UNIQUEIDENTIFIER    NULL,
    [CreatedOn]                      DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                     UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]                     DATETIMEOFFSET      NULL,
    [IsActive]                       BIT                 DEFAULT 1,
    CONSTRAINT FK_Pharmacy_Hospital FOREIGN KEY (HospitalId) REFERENCES [dbo].[Hospital](HospitalId) 
);