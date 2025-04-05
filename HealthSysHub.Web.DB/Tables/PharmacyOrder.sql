﻿CREATE TABLE [dbo].[PharmacyOrder]
(
    [PharmacyOrderId]          UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [PharmacyOrderRequestId]   UNIQUEIDENTIFIER    NULL,
    [ItemQty]                  BIGINT              NULL,
    [TotalAmount]              DECIMAL(22,11)      NULL,
    [DiscountAmount]           DECIMAL(22,11)      NULL,
    [FinalAmount]              DECIMAL(22,11)      NULL,
    [BalanceAmount]            DECIMAL(22,11)      NULL,
    [Notes]                    VARCHAR(MAX)        NULL,
    [CreatedBy]                UNIQUEIDENTIFIER    NULL,
    [CreatedOn]                DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]               UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]              DATETIMEOFFSET      NULL,
    [IsActive]                 BIT                 DEFAULT 1,

    -- Foreign key constraint
    CONSTRAINT [FK_PharmacyOrder_PharmacyOrderRequest] FOREIGN KEY ([PharmacyOrderRequestId]) 
        REFERENCES [dbo].[PharmacyOrderRequest]([PharmacyOrderRequestId])
)