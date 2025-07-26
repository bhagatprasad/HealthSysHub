CREATE TABLE [dbo].[Room] 
(
    [RoomId]        UNIQUEIDENTIFIER    NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [RoomNumber]    NVARCHAR(20)        NULL UNIQUE,
    [RoomTypeId]    UNIQUEIDENTIFIER    NULL,
    [FloorNumber]   INT                 NULL,
    [Wing]          NVARCHAR(20)        NULL,
    [BedCount]      INT                 NULL,
    [IsOccupied]    BIT                 DEFAULT 0,
    [LastCleaned]   DATETIME            NULL,
    [HospitalId]    UNIQUEIDENTIFIER    NULL,
    [CreatedBy]        UNIQUEIDENTIFIER   NULL,
    [CreatedOn]        DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]       UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]       DATETIMEOFFSET     NULL,
    [IsActive]         BIT                DEFAULT 1
);