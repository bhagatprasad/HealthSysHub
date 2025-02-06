CREATE TABLE [dbo].[Department]
(
	[DepartmentId]      UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [DepartmentName]    VARCHAR(200)       NOT NULL,
    [DepartmentDescription] VARCHAR(500)   NULL,
    [CreatedBy]         UNIQUEIDENTIFIER   NULL,
    [CreatedOn]         DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]        DATETIMEOFFSET     NULL,
    [IsActive]          BIT                DEFAULT 1
)
