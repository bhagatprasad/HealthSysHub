CREATE TABLE [dbo].[LabStaff]
(
    [StaffId]           uniqueidentifier        NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [FirstName]         varchar(250)                NULL,
    [LastName]          varchar(250)                NULL,
    [Designation]       varchar(250)                NULL,
    [PhoneNumber]       varchar(14)                 NULL,
    [Email]             varchar(250)                NULL,
    [HospitalId]        uniqueidentifier            NULL,
    [LabId]             uniqueidentifier            NULL,
    [CreatedBy]         uniqueidentifier            NULL,
    [CreatedOn]         datetimeoffset              NULL DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        uniqueidentifier            NULL,
    [ModifiedOn]        datetimeoffset              NULL,
    [IsActive]          bit                         NULL DEFAULT 1,
    FOREIGN KEY (LabId) REFERENCES Lab(LabId)
);