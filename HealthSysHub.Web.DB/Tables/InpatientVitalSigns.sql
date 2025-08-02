CREATE TABLE [dbo].[InpatientVitalSigns]
(
    [VitalSignId]        UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [InpatientId]       UNIQUEIDENTIFIER NOT NULL,
    [RecordedBy]        UNIQUEIDENTIFIER NULL,
    [RecordedOn]        DATETIMEOFFSET   NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [Temperature]       DECIMAL(5,2)     NULL, -- in Celsius
    [BloodPressure]     VARCHAR(20)      NULL, -- "120/80"
    [PulseRate]         INT              NULL, -- beats per minute
    [RespiratoryRate]   INT              NULL, -- breaths per minute
    [OxygenSaturation]  DECIMAL(5,2)     NULL, -- percentage
    [Height]           DECIMAL(5,2)     NULL, -- in cm
    [Weight]           DECIMAL(5,2)     NULL, -- in kg
    [Notes]            VARCHAR(MAX)     NULL,
    
    CONSTRAINT [FK_InpatientVitalSigns_Inpatient] FOREIGN KEY ([InpatientId]) 
        REFERENCES [dbo].[Inpatient]([InpatientId])
);