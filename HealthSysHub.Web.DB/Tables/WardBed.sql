CREATE TABLE [dbo].[WardBed]
(
    [BedId]               UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [WardId]             UNIQUEIDENTIFIER NOT NULL,
    [BedNumber]          VARCHAR(20)      NOT NULL,
    [BedType]            VARCHAR(50)      NOT NULL, -- 'Regular', 'ICU', 'Ventilator', etc.
    [Status]             VARCHAR(20)      NOT NULL DEFAULT 'Available', -- 'Available', 'Occupied', 'Maintenance'
    [CreatedBy]          UNIQUEIDENTIFIER NULL,
    [CreatedOn]          DATETIMEOFFSET   DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]         UNIQUEIDENTIFIER NULL,
    [ModifiedOn]         DATETIMEOFFSET   NULL,
    [IsActive]           BIT              DEFAULT 1,
    
    CONSTRAINT [FK_Bed_Ward] FOREIGN KEY ([WardId]) 
        REFERENCES [dbo].[Ward]([WardId])
);