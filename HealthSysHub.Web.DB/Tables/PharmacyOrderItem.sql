CREATE TABLE [dbo].[PharmacyOrderItem]
(
    [PharmacyOrderItemId]   UNIQUEIDENTIFIER    NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PharmacyId]            UNIQUEIDENTIFIER    NULL,
    [PharmacyOrderId]       UNIQUEIDENTIFIER    NULL,
    [MedicineId]            UNIQUEIDENTIFIER    NULL,
    [ItemQty]               BIGINT              NULL,
    [UnitPrice]             DECIMAL(22, 11)     NULL,
    [TotalAmount]           DECIMAL(22, 11)     NULL,
    [CreatedBy]             UNIQUEIDENTIFIER    NULL,
    [CreatedOn]             DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]            DATETIMEOFFSET      NULL,
    [IsActive]              BIT                 DEFAULT 1,
    
    -- Foreign key constraints
    CONSTRAINT [FK_PharmacyOrderItem_PharmacyOrder] FOREIGN KEY ([PharmacyOrderId]) 
        REFERENCES [dbo].[PharmacyOrder]([PharmacyOrderId])
        ON DELETE CASCADE,  -- Optional: cascade delete if order is deleted
    
    CONSTRAINT [FK_PharmacyOrderItem_Medicine] FOREIGN KEY ([MedicineId]) 
        REFERENCES [dbo].[Medicine]([MedicineId])
)