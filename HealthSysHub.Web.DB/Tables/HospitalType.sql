CREATE TABLE [dbo].[HospitalType]
(
	[HospitalTypeId]         UNIQUEIDENTIFIER       PRIMARY KEY     DEFAULT     NEWID(),
    [HospitalTypeName]       NVARCHAR(100)          NOT NULL,
    [Description]            NVARCHAR(255)          NULL,
    [CreatedBy]              UNIQUEIDENTIFIER       NULL,
    [CreatedOn]              DATETIMEOFFSET         DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]             UNIQUEIDENTIFIER       NULL,
    [ModifiedOn]             DATETIMEOFFSET         NULL,
    [IsActive]               BIT                    DEFAULT 1
)
