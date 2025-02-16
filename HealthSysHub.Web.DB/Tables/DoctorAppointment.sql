CREATE TABLE [dbo].[DoctorAppointment]
(
    [AppointmentId]             UNIQUEIDENTIFIER        NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]                UNIQUEIDENTIFIER            NULL,
    [DoctorId]                  UNIQUEIDENTIFIER            NULL,
    [AppointmentDate]           DATE                        NULL,
    [AppointmentTime]           TIME                        NULL,
    [PatientName]               VARCHAR(100)                NULL,
    [PatientPhone]              VARCHAR(100)                NULL,
    [ComingFrom]                VARCHAR(100)                NULL,
    [HealthIssue]               VARCHAR(255)                NULL,
    [TokenNo]                   INT                         NOT NULL,
    [Amount]                    DECIMAL(10, 2)              NULL,
    [PaymentType]               VARCHAR(50)                 NULL,
    [PaymentReference]          VARCHAR(100)                NULL,
    [Status]                    VARCHAR(100)                NULL,
    [StatusMessage]             VARCHAR(max)                NULL,
    [CreatedBy]                 UNIQUEIDENTIFIER            NULL,
    [CreatedOn]                 DATETIMEOFFSET              NULL DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                UNIQUEIDENTIFIER            NULL,
    [ModifiedOn]                DATETIMEOFFSET              NULL,
    [IsActive]                  BIT                         NULL DEFAULT 1
);