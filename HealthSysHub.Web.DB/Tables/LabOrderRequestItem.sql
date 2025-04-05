CREATE TABLE [dbo].[LabOrderRequestItem]
(
    [LabOrderRequestItemId]  UNIQUEIDENTIFIER    PRIMARY KEY DEFAULT NEWID(),
    [LabOrderRequestId]      UNIQUEIDENTIFIER    NULL,
    [HospitalId]            UNIQUEIDENTIFIER    NULL,
    [TestId]                UNIQUEIDENTIFIER    NULL,
    [ItemQty]               DECIMAL(18,2)       NULL, 
    [CreatedBy]             UNIQUEIDENTIFIER    NULL,
    [CreatedOn]             DATETIMEOFFSET      DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER    NULL,
    [ModifiedOn]            DATETIMEOFFSET      NULL,
    [IsActive]              BIT                 DEFAULT 1,
    
    -- Foreign key constraints
    CONSTRAINT [FK_LabOrderRequestItem_LabOrderRequest] FOREIGN KEY ([LabOrderRequestId]) 
        REFERENCES [dbo].[LabOrderRequest]([LabOrderRequestId]),
    
    CONSTRAINT [FK_LabOrderRequestItem_Hospital] FOREIGN KEY ([HospitalId]) 
        REFERENCES [dbo].[Hospital]([HospitalId]),
    
    CONSTRAINT [FK_LabOrderRequestItem_Test] FOREIGN KEY ([TestId]) 
        REFERENCES [dbo].[LabTest]([TestId])
)