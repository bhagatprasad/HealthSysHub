CREATE TABLE [dbo].[PharmacyMedicine]
(
    [MedicineId]         UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [MedicineName]       VARCHAR(200)       NOT NULL,
    [GenericName]        VARCHAR(200)       NULL,
    [DosageForm]         VARCHAR(100)       NULL, 
    [Strength]           VARCHAR(100)       NULL, 
    [Manufacturer]       VARCHAR(200)       NULL,
    [BatchNumber]        VARCHAR(100)       NULL,
    [ExpiryDate]         DATETIMEOFFSET     NULL,
    [PricePerUnit]       DECIMAL(10,2)      NOT NULL,
    [StockQuantity]      INT                NULL DEFAULT 0,
    [PharmacyId]         UNIQUEIDENTIFIER    NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER   NULL,
    [CreatedOn]          DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]         UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]         DATETIMEOFFSET     NULL,
    [IsActive]           BIT                DEFAULT 1
);