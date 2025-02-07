CREATE TABLE [dbo].[HospitalDepartment]
(
    [HospitalDepartmentId]                      UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]                                UNIQUEIDENTIFIER NULL,
    [DepartmentId]                              UNIQUEIDENTIFIER NULL,
    [HeadOfDepartment]                          VARCHAR(100)     NULL,
    [CreatedBy]                                 UNIQUEIDENTIFIER NULL,
    [CreatedOn]                                 DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                                UNIQUEIDENTIFIER NULL,
    [ModifiedOn]                                DATETIMEOFFSET NULL,
    [IsActive]                                  BIT DEFAULT 1,
    CONSTRAINT FK_HospitalDepartment_Hospital FOREIGN KEY (HospitalId) REFERENCES [dbo].[Hospital](HospitalID),
    CONSTRAINT FK_HospitalDepartment_Department FOREIGN KEY (DepartmentId) REFERENCES [dbo].[Department](DepartmentId)
);