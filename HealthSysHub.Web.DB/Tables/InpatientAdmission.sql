CREATE TABLE [dbo].[InpatientAdmission]
(
    [AdmissionId]          UNIQUEIDENTIFIER         NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PatientId]            UNIQUEIDENTIFIER         NULL,
    [HospitalId]           UNIQUEIDENTIFIER         NULL,
    [AdmittingDoctorId]    UNIQUEIDENTIFIER         NULL,
    [AdmissionDate]        DATETIMEOffSet           NULL DEFAULT GETDATE(),
    [AdmissionType]        NVARCHAR(50)             NULL,
    [ReasonForAdmission]   NVARCHAR(500)            NULL,
    [ExpectedStayDuration] INT                      NULL,
    [DischargeDate]        DATETIMEOffSet           NULL,
    [DischargeStatus]      NVARCHAR(50)             NULL,
    [Status]               NVARCHAR(max)            NULL,
    [CreatedBy]            UNIQUEIDENTIFIER         NULL,
    [CreatedOn]            DATETIMEOFFSET           DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]           UNIQUEIDENTIFIER         NULL,
    [ModifiedOn]           DATETIMEOFFSET           NULL,
    [IsActive]             BIT                      DEFAULT 1,
);