CREATE DATABASE AryaPangestu_20250318;
GO

USE AryaPangestu_20250318;
GO

-- Create Tables
CREATE TABLE [dbo].[Shipment](
    [ShipmentId] [int] IDENTITY(1,1) NOT NULL,
    [TrackingNumber] [nvarchar](20) NOT NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    -- Sender Info
    [SenderName] [nvarchar](100) NOT NULL,
    [SenderPhone] [nvarchar](20) NOT NULL,
    [SenderAddress] [nvarchar](255) NOT NULL,
    [SenderPostCode] [nvarchar](10) NOT NULL,
    -- Recipient Info
    [RecipientName] [nvarchar](100) NOT NULL,
    [RecipientPhone] [nvarchar](20) NOT NULL,
    [RecipientAddress] [nvarchar](255) NOT NULL,
    [RecipientPostCode] [nvarchar](10) NOT NULL,
    -- Package Info
    [Weight] [decimal](10, 2) NOT NULL,
    [Length] [decimal](10, 2) NOT NULL,
    [Width] [decimal](10, 2) NOT NULL,
    [Height] [decimal](10, 2) NOT NULL,
    [CurrentStatus] [nvarchar](50) NOT NULL DEFAULT 'Created',
    CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED ([ShipmentId] ASC),
    CONSTRAINT [UK_Shipment_TrackingNumber] UNIQUE ([TrackingNumber])
);

CREATE TABLE [dbo].[ShipmentStatusHistory](
    [StatusId] [int] IDENTITY(1,1) NOT NULL,
    [ShipmentId] [int] NOT NULL,
    [Status] [nvarchar](50) NOT NULL,
    [StatusDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [Notes] [nvarchar](255) NULL,
    CONSTRAINT [PK_ShipmentStatusHistory] PRIMARY KEY CLUSTERED ([StatusId] ASC),
    CONSTRAINT [FK_ShipmentStatusHistory_Shipment] FOREIGN KEY([ShipmentId]) 
        REFERENCES [dbo].[Shipment] ([ShipmentId])
);

-- Insert Sample Shipment Data
INSERT INTO [dbo].[Shipment] (
    [TrackingNumber], 
    [CreatedDate], 
    [SenderName], 
    [SenderPhone], 
    [SenderAddress], 
    [SenderPostCode], 
    [RecipientName], 
    [RecipientPhone], 
    [RecipientAddress], 
    [RecipientPostCode], 
    [Weight], 
    [Length], 
    [Width], 
    [Height], 
    [CurrentStatus]
)
VALUES (
    'KEID0000001', 
    '2025-03-16 08:00:00', 
    'John Doe', 
    '08123456789', 
    'Jl. Sudirman No. 123, Jakarta', 
    '12345', 
    'Jane Smith', 
    '08987654321', 
    'Jl. Gatot Subroto No. 456, Bandung', 
    '54321', 
    5.5, 
    30.0, 
    20.0, 
    15.0, 
    'POD'
);

-- Get the ShipmentId
DECLARE @ShipmentId INT = SCOPE_IDENTITY();

-- Insert Status History for Sample Shipment
-- Shipment Pick Up
INSERT INTO [dbo].[ShipmentStatusHistory] (
    [ShipmentId], 
    [Status], 
    [StatusDate], 
    [Notes]
)
VALUES (
    @ShipmentId, 
    'Shipment Pick Up', 
    '2025-03-16 09:30:00', 
    'Package picked up from sender'
);

-- On Delivery
INSERT INTO [dbo].[ShipmentStatusHistory] (
    [ShipmentId], 
    [Status], 
    [StatusDate], 
    [Notes]
)
VALUES (
    @ShipmentId, 
    'On Delivery', 
    '2025-03-17 13:45:00', 
    'Package in transit to destination'
);

-- POD
INSERT INTO [dbo].[ShipmentStatusHistory] (
    [ShipmentId], 
    [Status], 
    [StatusDate], 
    [Notes]
)
VALUES (
    @ShipmentId, 
    'POD', 
    '2025-03-18 11:20:00', 
    'Package delivered to recipient'
);