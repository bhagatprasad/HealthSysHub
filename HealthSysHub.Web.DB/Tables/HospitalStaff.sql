CREATE TABLE [dbo].[HospitalStaff]
(
	[StaffId]           uniqueidentifier    NOT         NULL    PRIMARY KEY     DEFAULT NEWID(),
    [HospitalId]        uniqueidentifier    NOT         NULL ,
    [FirstName]         varchar(250)                    NULL,
	[LastName]          varchar(250)                    NULL,
	[Email]             varchar(250)                    NULL,
	[Phone]             varchar(14)                     NULL,
    [Designation]       varchar(250)                    NULL, 
    [RoleId]            uniqueidentifier                NULL,
    [DepartmentId]      uniqueidentifier                NULL,
    [SpecializationId]  uniqueidentifier                NULL,   
    [LicenseNumber]     varchar(100)                    NULL,   
    [CreatedBy]         uniqueidentifier                NULL,
    [CreatedOn]         datetimeoffset                  NULL,
    [ModifiedBy]        uniqueidentifier                NULL,
    [ModifiedOn]        datetimeoffset                  NULL,
    [IsActive]          bit                             NULL      DEFAULT 1
)
