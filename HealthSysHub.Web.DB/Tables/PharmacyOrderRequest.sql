CREATE TABLE [dbo].[PharmacyOrderRequest]
(
    [PharmacyOrderRequestId]    UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [PatientPrescriptionId]     UNIQUEIDENTIFIER    NULL,
    [HospitalId]                UNIQUEIDENTIFIER    NULL,
    [PatientId]                 UNIQUEIDENTIFIER    NULL,
    [HospitalName]              VARCHAR(MAX)        NULL,
    [DoctorName]                VARCHAR(MAX)        NULL,
    [Name]                      VARCHAR(MAX)        NULL,
    [Phone]                     VARCHAR(MAX)        NULL,
    [Notes]                     VARCHAR(MAX)        NULL,
    [CreatedBy]                 UNIQUEIDENTIFIER    NULL,
    [CreatedOn]                 DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]                DATETIMEOFFSET      NULL,
    [IsActive]                  BIT                 DEFAULT 1,

    -- Foreign key constraints
    CONSTRAINT [FK_PharmacyOrderRequest_PatientPrescription] FOREIGN KEY ([PatientPrescriptionId]) 
        REFERENCES [dbo].[PatientPrescription]([PatientPrescriptionId]),

    CONSTRAINT [FK_PharmacyOrderRequest_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId]),

    CONSTRAINT [FK_PharmacyOrderRequest_Patient] FOREIGN KEY ([PatientId]) 
        REFERENCES [dbo].[Patient]([PatientId])
)