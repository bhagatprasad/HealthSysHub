CREATE TABLE [dbo].[HospitalSpecialty]
(
    [HospitalSpecialtyId]   UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]            UNIQUEIDENTIFIER   NULL,
    [SpecializationId]      UNIQUEIDENTIFIER   NULL,
    [CreatedBy]             UNIQUEIDENTIFIER   NULL,
    [CreatedOn]             DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]            DATETIMEOFFSET     NULL,
    [IsActive]              BIT                DEFAULT 1,
    CONSTRAINT FK_HospitalSpecialty_Hospital FOREIGN KEY (HospitalId) REFERENCES [dbo].[Hospital](HospitalId),
    CONSTRAINT FK_HospitalSpecialty_Specialization FOREIGN KEY (SpecializationId) REFERENCES [dbo].[Specialization](SpecializationId)
);