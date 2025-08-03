CREATE TABLE [dbo].[Ward]
(
    [WardId]              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [HospitalId]          UNIQUEIDENTIFIER NOT NULL,
    [WardName]            VARCHAR(100)     NOT NULL,
    [WardType]            VARCHAR(50)      NOT NULL, -- 'General', 'ICU', 'Pediatric', etc.
    [Capacity]            INT              NOT NULL,
    [CurrentOccupancy]    INT              DEFAULT 0,
    [Description]         VARCHAR(MAX)     NULL,
    [CreatedBy]           UNIQUEIDENTIFIER NULL,
    [CreatedOn]           DATETIMEOFFSET   DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]          UNIQUEIDENTIFIER NULL,
    [ModifiedOn]         DATETIMEOFFSET   NULL,
    [IsActive]            BIT              DEFAULT 1,
    
    CONSTRAINT [FK_Ward_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId])
);