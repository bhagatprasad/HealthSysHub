CREATE TABLE [dbo].[Bed]
(
    [BedId]         UNIQUEIDENTIFIER    NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [RoomId]        UNIQUEIDENTIFIER    NULL,
    [BedNumber]     NVARCHAR(10)        NULL,
    [BedType]       NVARCHAR(50)        NULL,
    [IsOccupied]    BIT                 DEFAULT 0,
    [IsActive]      BIT                 DEFAULT 1,
    [CreatedBy]     UNIQUEIDENTIFIER    NULL,
    [CreatedOn]     DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]    UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]    DATETIMEOFFSET      NULL
);