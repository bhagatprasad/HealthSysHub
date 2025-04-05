CREATE TABLE [dbo].[LabOrderItem]
(
    [LabOrderItemId]    UNIQUEIDENTIFIER    NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [LabOrderId]        UNIQUEIDENTIFIER    NULL,
    [TestId]            UNIQUEIDENTIFIER    NULL,
    [ItemQty]           BIGINT              NULL,
    [UnitPrice]         DECIMAL(22, 11)     NULL,
    [TotalAmount]       DECIMAL(22, 11)     NULL,
    [CreatedBy]         UNIQUEIDENTIFIER    NULL,
    [CreatedOn]         DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]        DATETIMEOFFSET      NULL,
    [IsActive]          BIT                 DEFAULT 1,
    
    -- Foreign key constraints
    CONSTRAINT [FK_LabOrderItem_LabOrder] FOREIGN KEY ([LabOrderId]) 
        REFERENCES [dbo].[LabOrder]([LabOrderId]),
    
    CONSTRAINT [FK_LabOrderItem_Test] FOREIGN KEY ([TestId]) 
        REFERENCES [dbo].[LabTest]([TestId])
)