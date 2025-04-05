CREATE TABLE [dbo].[PatientVital]
(
	[VitalId]                  UNIQUEIDENTIFIER        PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]               UNIQUEIDENTIFIER        NULL,
    [PatientId]                UNIQUEIDENTIFIER        NULL,
    [ConsultationId]		   UNIQUEIDENTIFIER        NULL,
    [BodyTemperature]          VARCHAR(MAX)            NULL,
    [HeartRate]                VARCHAR(MAX)            NULL,
    [BloodPressure]            VARCHAR(MAX)            NULL,
    [RespiratoryRate]          VARCHAR(MAX)            NULL,
    [OxygenSaturation]         VARCHAR(MAX)            NULL,
    [Height]                   VARCHAR(MAX)            NULL,
    [Weight]                   VARCHAR(MAX)            NULL,
    [BMI]                      VARCHAR(MAX)            NULL,
    [Notes]                    VARCHAR(MAX)            NULL,
    [CreatedBy]				   UNIQUEIDENTIFIER        NULL,
    [CreatedOn]				   DATETIMEOFFSET			DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]			   UNIQUEIDENTIFIER	       NULL,
    [ModifiedOn]			   DATETIMEOFFSET	       NULL,
    [IsActive]				   BIT                     DEFAULT 1,
    FOREIGN KEY (HospitalId) REFERENCES Hospital(HospitalId),
    FOREIGN KEY (PatientId) REFERENCES Patient(PatientId)
)
