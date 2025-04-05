CREATE TABLE [dbo].[PatientPrescription]
(
    [PatientPrescriptionId]  UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]            UNIQUEIDENTIFIER    NULL,
    [PatientId]             UNIQUEIDENTIFIER    NULL,
    [ConsultationId]        UNIQUEIDENTIFIER    NULL,
    [Treatment]             VARCHAR(MAX)        NULL,
    [Advoice]               VARCHAR(MAX)        NULL,
    [Diognasys]             VARCHAR(MAX)        NULL,  -- Note: Consider renaming to 'Diagnosis' for consistency
    [Notes]                 VARCHAR(MAX)        NULL,
    [FallwoUpOn]            DATETIMEOFFSET      NULL,  -- Note: Consider renaming to 'FollowUpOn' for correct spelling
    [CreatedBy]             UNIQUEIDENTIFIER    NULL,
    [CreatedOn]             DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]            DATETIMEOFFSET      NULL,
    [IsActive]              BIT                 DEFAULT 1,
    
    -- Foreign key constraints
    CONSTRAINT [FK_PatientPrescription_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId]),
    
    CONSTRAINT [FK_PatientPrescription_Patient] FOREIGN KEY ([PatientId]) 
        REFERENCES [dbo].[Patient]([PatientId]),
    
    CONSTRAINT [FK_PatientPrescription_Consultation] FOREIGN KEY ([ConsultationId]) 
        REFERENCES [dbo].[Consultation]([ConsultationId])
)