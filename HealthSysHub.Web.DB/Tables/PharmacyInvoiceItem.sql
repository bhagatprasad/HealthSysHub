CREATE TABLE [dbo].[PharmacyInvoiceItem]
(
	[InvoiceItemId]                     UNIQUEIDENTIFIER                PRIMARY KEY DEFAULT NEWID(),
    [InvoiceId]                         UNIQUEIDENTIFIER                NULL,
	[PharmacyOrderId]                   UNIQUEIDENTIFIER                NULL,
    [HospitalId]                        UNIQUEIDENTIFIER                NULL,
    [PharmacyId]                        UNIQUEIDENTIFIER                NULL,
    [PharmacyOrderRequestId]            UNIQUEIDENTIFIER                NULL,
    [MedicineId]                        UNIQUEIDENTIFIER                NULL,
    [ItemQty]                           BIGINT                          NULL,
    [UnitPrice]                         DECIMAL(22, 11)                 NULL,
    [TotalAmount]                       DECIMAL(22, 11)                 NULL,
    [CreatedBy]                         UNIQUEIDENTIFIER                NULL,
    [CreatedOn]                         DATETIMEOFFSET                  NULL DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                        UNIQUEIDENTIFIER                NULL,
    [ModifiedOn]                        DATETIMEOFFSET                  NULL,
    [IsActive]                          BIT                             NULL  DEFAULT 1
)
