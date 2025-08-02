CREATE TABLE [dbo].[Inpatient]
(
    [InpatientId]          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [PatientId]            UNIQUEIDENTIFIER NOT NULL,
    [HospitalId]           UNIQUEIDENTIFIER NOT NULL,
    [AdmissionDate]        DATETIMEOFFSET   NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [DischargeDate]        DATETIMEOFFSET   NULL,
    [WardId]               UNIQUEIDENTIFIER NULL,
    [BedId]                UNIQUEIDENTIFIER NULL,
    [AdmittingDoctorId]    UNIQUEIDENTIFIER NULL,
    [CurrentStatus]        VARCHAR(50)      NOT NULL, -- 'Admitted', 'Discharged', 'Transferred', etc.
    [ReasonForAdmission]   VARCHAR(MAX)     NULL,
    [ExpectedStayDuration] INT              NULL, -- in days
    [CreatedBy]            UNIQUEIDENTIFIER NULL,
    [CreatedOn]            DATETIMEOFFSET   DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]           UNIQUEIDENTIFIER NULL,
    [ModifiedOn]          DATETIMEOFFSET   NULL,
    [IsActive]             BIT              DEFAULT 1,
    
    CONSTRAINT [FK_Inpatient_Patient] FOREIGN KEY ([PatientId]) 
        REFERENCES [dbo].[Patient]([PatientId]),
        
    CONSTRAINT [FK_Inpatient_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId]),
        
    CONSTRAINT [FK_Inpatient_Ward] FOREIGN KEY ([WardId]) 
        REFERENCES [dbo].[Ward]([WardId]),
        
    CONSTRAINT [FK_Inpatient_Bed] FOREIGN KEY ([BedId]) 
        REFERENCES [dbo].[WardBed]([BedId]),
        
    CONSTRAINT [FK_Inpatient_Doctor] FOREIGN KEY ([AdmittingDoctorId]) 
        REFERENCES [dbo].[Doctor]([DoctorId])
);