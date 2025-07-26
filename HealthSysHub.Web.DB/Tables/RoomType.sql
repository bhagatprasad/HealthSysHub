CREATE TABLE [dbo].[RoomType]
(
    [RoomTypeId]       UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [TypeName]         VARCHAR(50)        NULL,
    [Description]      VARCHAR(255)       NULL,
    [BaseRate]         DECIMAL(10,2)      NULL,
    [CreatedBy]        UNIQUEIDENTIFIER   NULL,
    [CreatedOn]        DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]       UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]       DATETIMEOFFSET     NULL,
    [IsActive]         BIT                DEFAULT 1
)