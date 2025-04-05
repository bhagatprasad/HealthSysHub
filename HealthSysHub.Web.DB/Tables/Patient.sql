CREATE TABLE [dbo].[Patient]
(
	[PatientId]							UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[HospitalId]					    UNIQUEIDENTIFIER     NULL,
	[ConsultationId]					UNIQUEIDENTIFIER     NULL,
	[PatientTypeId]						UNIQUEIDENTIFIER     NULL,
	[HealthIssue]						varchar(max)         NULL,
	[Name]								varchar(max)         NULL,
	[Phone]								varchar(max)         NULL,
	[AttenderPhone]						varchar(max)         NULL,
	[Age]                               varchar(max)         NULL,
	[Gender]							varchar(max)         NULL,
	[Address]							varchar(max)         NULL,
	[CreatedBy]							UNIQUEIDENTIFIER     NULL,
    [CreatedOn]							DATETIMEOFFSET			DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]						UNIQUEIDENTIFIER	 NULL,
    [ModifiedOn]						DATETIMEOFFSET	     NULL,
    [IsActive]							BIT                DEFAULT 1,
	FOREIGN KEY (HospitalId) REFERENCES Hospital(HospitalId),
	FOREIGN KEY (ConsultationId) REFERENCES Consultation(ConsultationId),
	FOREIGN KEY (PatientTypeId) REFERENCES PatientType(PatientTypeId)
)
