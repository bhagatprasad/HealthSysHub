CREATE TABLE [dbo].[PharmacyPayment]
(
	[PaymentId]         UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [PharmacyOrderId]                   UNIQUEIDENTIFIER                NULL,
    [HospitalId]                        UNIQUEIDENTIFIER                NULL,
    [PharmacyId]                        UNIQUEIDENTIFIER                NULL,
    [PaymentNumber]     VARCHAR(50)         NOT NULL UNIQUE,
    [PaymentDate]       DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [PaymentMethod]     VARCHAR(50)         NOT NULL  CHECK ([PaymentMethod] IN ('Cash', 'Credit Card', 'Debit Card', 'Check', 'Bank Transfer', 'Online', 'Insurance')),
    [PaymentAmount]     DECIMAL(22,11)      NOT NULL,
    [ReferenceNumber]   VARCHAR(100)        NULL, -- Transaction ID, check number, etc.
    [Status]            VARCHAR(50)         NOT NULL  CHECK ([Status] IN ('Pending', 'Completed', 'Failed', 'Refunded', 'Partially Refunded')),
    [PaymentGateway]    VARCHAR(100)        NULL, -- For online payments
    [GatewayResponse]   VARCHAR(MAX)        NULL, -- Raw gateway response
    [Notes]             VARCHAR(MAX)        NULL,
    [CreatedBy]         UNIQUEIDENTIFIER    NULL,
    [CreatedOn]         DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]        DATETIMEOFFSET      NULL,
    [IsActive]          BIT                 DEFAULT 1
)
