CREATE TABLE [dbo].[PharmacyInvoice]
(
	[InvoiceId]                         UNIQUEIDENTIFIER                PRIMARY KEY         DEFAULT NEWID(),
    [PharmacyOrderId]                   UNIQUEIDENTIFIER                NULL,
    [HospitalId]                        UNIQUEIDENTIFIER                NULL,
    [PharmacyId]                        UNIQUEIDENTIFIER                NULL,
    [PharmacyOrderRequestId]            UNIQUEIDENTIFIER                NULL,
    [InvoiceReferance]                  VARCHAR(MAX)                    NULL,
    [SubTotal]                          DECIMAL(22,11)                  NULL,
    [TaxAmount]                         DECIMAL(22,11)                  NULL                DEFAULT 0,
    [DiscountAmount]                    DECIMAL(22,11)                  NULL                DEFAULT 0,
    [TotalAmount]                       DECIMAL(22,11)                  NULL,
    [AmountPaid]                        DECIMAL(22,11)                  NULL                DEFAULT 0,
    [BalanceDue]                        DECIMAL(22,11)                  NULL,
    [Status]                            VARCHAR(50)                     NULL, -- e.g., 'Draft', 'Sent', 'Paid', 'Partial', 'Overdue', 'Cancelled'
    [Notes]                             VARCHAR(MAX)                    NULL,
    [CreatedBy]                         UNIQUEIDENTIFIER                NULL,
    [CreatedOn]                         DATETIMEOFFSET                  NULL                DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]                        UNIQUEIDENTIFIER                NULL,
    [ModifiedOn]                        DATETIMEOFFSET                  NULL,
    [IsActive]                          BIT                             NULL                DEFAULT 1
)
