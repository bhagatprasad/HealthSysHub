CREATE TABLE [dbo].[InpatientTreatmentPlan]
(
    [TreatmentPlanId]     UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [InpatientId]        UNIQUEIDENTIFIER NOT NULL,
    [DoctorId]          UNIQUEIDENTIFIER NOT NULL,
    [PlanDetails]       VARCHAR(MAX)     NOT NULL,
    [StartDate]         DATETIMEOFFSET   NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [ExpectedEndDate]   DATETIMEOFFSET   NULL,
    [Status]           VARCHAR(50)      NOT NULL DEFAULT 'Active', -- 'Active', 'Completed', 'Cancelled'
    [CreatedBy]        UNIQUEIDENTIFIER NULL,
    [CreatedOn]        DATETIMEOFFSET   DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]       UNIQUEIDENTIFIER NULL,
    [ModifiedOn]      DATETIMEOFFSET   NULL,
    [IsActive]         BIT              DEFAULT 1,
    
    CONSTRAINT [FK_InpatientTreatmentPlan_Inpatient] FOREIGN KEY ([InpatientId]) 
        REFERENCES [dbo].[Inpatient]([InpatientId]),
        
    CONSTRAINT [FK_InpatientTreatmentPlan_Doctor] FOREIGN KEY ([DoctorId]) 
        REFERENCES [dbo].[Doctor]([DoctorId])
);