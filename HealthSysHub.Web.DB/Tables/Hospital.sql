CREATE TABLE [dbo].[Hospital]
(
    [HospitalId]                     UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),  -- Unique identifier for the hospital
    [HospitalName]                   VARCHAR(100)       NULL,                               -- Name of the hospital
    [HospitalCode]                   VARCHAR(100)       NULL,                               -- Name of the hospital
    [RegistrationNumber]             VARCHAR(50)       NULL,                               -- Registration number of the hospital
    [Address]                        VARCHAR(255)       NULL,                               -- Full address of the hospital
    [City]                           VARCHAR(100)        NULL,                               -- City where the hospital is located
    [State]                          VARCHAR(100)        NULL,                               -- State where the hospital is located
    [Country]                        VARCHAR(100)        NULL,                               -- Country of the hospital
    [PostalCode]                     VARCHAR(20)         NULL,                               -- Postal code of the hospital
    [PhoneNumber]                    VARCHAR(20)         NULL,                               -- Contact phone number of the hospital
    [Email]                          VARCHAR(100)        NULL,                                   -- Email address of the hospital
    [Website]                        VARCHAR(100)        NULL,                                -- Website URL of the hospital
    [LogoUrl]                        VARCHAR(100)        NULL,                                -- Website URL of the hospital
    [HospitalTypeId]                 UNIQUEIDENTIFIER     NULL,
    [CreatedBy]                      UNIQUEIDENTIFIER   NULL,
    [CreatedOn]         DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]        DATETIMEOFFSET     NULL,
    [IsActive]          BIT                DEFAULT 1,
    CONSTRAINT FK_Hospital_HospitalType FOREIGN KEY (HospitalTypeId) REFERENCES [dbo].[HospitalType](HospitalTypeId) 
);