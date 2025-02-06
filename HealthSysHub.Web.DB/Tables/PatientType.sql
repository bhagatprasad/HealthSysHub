CREATE TABLE [dbo].[PatientType]
(
	[PatientTypeId]     UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PatientTypeName]   VARCHAR(100)       NULL,
    [Description]       VARCHAR(500)       NULL,
    [CreatedBy]         UNIQUEIDENTIFIER   NULL,
    [CreatedOn]         DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]        DATETIMEOFFSET     NULL,
    [IsActive]          BIT                DEFAULT 1
)
