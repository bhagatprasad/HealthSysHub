CREATE TABLE [dbo].[PharmacyOrderRequestItem]
(
    [PharmacyOrderRequestItemId]    UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [PharmacyOrderRequestId]        UNIQUEIDENTIFIER    NULL,
    [HospitalId]                    UNIQUEIDENTIFIER    NULL,
    [PharmacyId]                    UNIQUEIDENTIFIER    NULL,
    [MedicineId]                    UNIQUEIDENTIFIER    NULL,
    [ItemQty]                       DECIMAL(18,2)       NULL,  -- Added precision/scale for DECIMAL
    [Usage]                         VARCHAR(MAX)        NULL,
    [CreatedBy]                     UNIQUEIDENTIFIER    NULL,
    [CreatedOn]                     DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                    UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]                    DATETIMEOFFSET      NULL,
    [IsActive]                      BIT                 DEFAULT 1,

    -- Foreign key constraints
    CONSTRAINT [FK_PharmacyOrderRequestItem_PharmacyOrderRequest] FOREIGN KEY ([PharmacyOrderRequestId]) 
        REFERENCES [dbo].[PharmacyOrderRequest]([PharmacyOrderRequestId])
        ON DELETE CASCADE,  -- Optional: cascade delete if parent request is deleted

    CONSTRAINT [FK_PharmacyOrderRequestItem_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId]),
)