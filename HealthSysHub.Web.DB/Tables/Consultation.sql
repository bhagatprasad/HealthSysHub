CREATE TABLE [dbo].[Consultation]
(
    [ConsultationId]      UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [AppointmentId]      UNIQUEIDENTIFIER    NULL, 
    [HospitalId]         UNIQUEIDENTIFIER    NULL,
    [DoctorId]           UNIQUEIDENTIFIER    NULL,
    [Status]             varchar(max)        NULL,
    [CreatedBy]          UNIQUEIDENTIFIER    NULL,
    [CreatedOn]          DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]         UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]         DATETIMEOFFSET      NULL,
    [IsActive]           BIT                 DEFAULT 1,
   
    CONSTRAINT [FK_Consultation_Appointment] FOREIGN KEY ([AppointmentId]) 
        REFERENCES [dbo].[DoctorAppointment]([AppointmentId]),
    
    CONSTRAINT [FK_Consultation_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId]),
    
    CONSTRAINT [FK_Consultation_Doctor] FOREIGN KEY ([DoctorId]) 
        REFERENCES [dbo].[Doctor]([DoctorId])
)