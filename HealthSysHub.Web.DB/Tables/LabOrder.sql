CREATE TABLE [dbo].[LabOrder]
(
    [LabOrderId]            UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [LabOrderRequestId]     UNIQUEIDENTIFIER    NULL,
    [ItemQty]               BIGINT              NULL,
    [TotalAmount]           DECIMAL(22,11)      NULL,
    [DiscountAmount]        DECIMAL(22,11)      NULL,
    [FinalAmount]           DECIMAL(22,11)      NULL,
    [BalanceAmount]         DECIMAL(22,11)      NULL,
    [Notes]                 VARCHAR(MAX)        NULL,
    [Status]                VARCHAR(max)        NULL,
    [CreatedBy]             UNIQUEIDENTIFIER    NULL,
    [CreatedOn]             DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]            DATETIMEOFFSET      NULL,
    [IsActive]              BIT                 DEFAULT 1,
    CONSTRAINT [FK_LabOrder_LabOrderRequest] FOREIGN KEY ([LabOrderRequestId])  REFERENCES [dbo].[LabOrderRequest]([LabOrderRequestId])
)