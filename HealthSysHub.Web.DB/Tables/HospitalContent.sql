CREATE TABLE [dbo].[HospitalContent]
(
	[HospitalContentId]                         UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]                                UNIQUEIDENTIFIER NULL,
    [Description]                               VARCHAR(MAX),
    [CreatedBy]                                 UNIQUEIDENTIFIER NULL,
    [CreatedOn]                                 DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                                UNIQUEIDENTIFIER NULL,
    [ModifiedOn]                                DATETIMEOFFSET NULL,
    [IsActive]                                  BIT DEFAULT 1,
    CONSTRAINT FK_HospitalContent_Hospital FOREIGN KEY (HospitalId) REFERENCES [dbo].[Hospital](HospitalID)
)
