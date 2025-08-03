CREATE TABLE [dbo].[BedAssignment]
(
    [AssignmentId]        UNIQUEIDENTIFIER    NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [AdmissionId]         UNIQUEIDENTIFIER    NULL,
    [WardId]              UNIQUEIDENTIFIER    NULL,
    [BedId]               UNIQUEIDENTIFIER    NULL,
    [AssignedDate]        DATETIMEOFFSET            NULL DEFAULT GETDATE(),
    [DischargedDate]      DATETIMEOFFSET            NULL,
    [IsActive]            BIT                 DEFAULT 1,
    [CreatedBy]           UNIQUEIDENTIFIER    NULL,
    [CreatedDate]         DATETIMEOFFSET            DEFAULT GETDATE(),
    [ModifiedBy]          UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]          DATETIMEOFFSET            NULL
);