CREATE TABLE [dbo].[InpatientMedication]
(
    [MedicationId]       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [InpatientId]       UNIQUEIDENTIFIER NOT NULL,
    [MedicineId]        UNIQUEIDENTIFIER NOT NULL,
    [DoctorId]          UNIQUEIDENTIFIER NULL,
    [Dosage]            VARCHAR(100)     NOT NULL,
    [Frequency]         VARCHAR(100)     NOT NULL,
    [StartDate]         DATETIMEOFFSET   NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [EndDate]           DATETIMEOFFSET   NULL,
    [Status]           VARCHAR(50)      NOT NULL DEFAULT 'Active', -- 'Active', 'Completed', 'Stopped'
    [Notes]            VARCHAR(MAX)     NULL,
    [CreatedBy]        UNIQUEIDENTIFIER NULL,
    [CreatedOn]        DATETIMEOFFSET   DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]       UNIQUEIDENTIFIER NULL,
    [ModifiedOn]      DATETIMEOFFSET   NULL,
    [IsActive]         BIT              DEFAULT 1,
    
    CONSTRAINT [FK_InpatientMedication_Inpatient] FOREIGN KEY ([InpatientId]) 
        REFERENCES [dbo].[Inpatient]([InpatientId]),
        
    CONSTRAINT [FK_InpatientMedication_Medicine] FOREIGN KEY ([MedicineId]) 
        REFERENCES [dbo].[Medicine]([MedicineId]),
        
    CONSTRAINT [FK_InpatientMedication_Doctor] FOREIGN KEY ([DoctorId]) 
        REFERENCES [dbo].[Doctor]([DoctorId])
);