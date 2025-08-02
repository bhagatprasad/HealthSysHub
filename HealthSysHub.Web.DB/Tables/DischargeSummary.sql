CREATE TABLE [dbo].[DischargeSummary]
(
    [DischargeSummaryId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [InpatientId]       UNIQUEIDENTIFIER NOT NULL,
    [DischargingDoctorId] UNIQUEIDENTIFIER NOT NULL,
    [DischargeDate]     DATETIMEOFFSET   NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [Diagnosis]         VARCHAR(MAX)     NOT NULL,
    [TreatmentSummary]  VARCHAR(MAX)     NOT NULL,
    [FollowUpInstructions] VARCHAR(MAX)  NULL,
    [FollowUpDate]      DATETIMEOFFSET   NULL,
    [StatusAtDischarge] VARCHAR(MAX)     NULL,
    [CreatedBy]        UNIQUEIDENTIFIER NULL,
    [CreatedOn]        DATETIMEOFFSET   DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]       UNIQUEIDENTIFIER NULL,
    [ModifiedOn]      DATETIMEOFFSET   NULL,
    [IsActive]         BIT              DEFAULT 1,
    
    CONSTRAINT [FK_DischargeSummary_Inpatient] FOREIGN KEY ([InpatientId]) 
        REFERENCES [dbo].[Inpatient]([InpatientId]),
        
    CONSTRAINT [FK_DischargeSummary_Doctor] FOREIGN KEY ([DischargingDoctorId]) 
        REFERENCES [dbo].[Doctor]([DoctorId])
);