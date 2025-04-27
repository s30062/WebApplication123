

CREATE TABLE Devices (
                         Id INT PRIMARY KEY IDENTITY(1,1),
                         Name NVARCHAR(100) NOT NULL,
                         Type NVARCHAR(50) NOT NULL,
                         IsTurnedOn BIT NOT NULL,
                         IpAddress NVARCHAR(100) NULL,
                         NetworkName NVARCHAR(100) NULL,
                         OperatingSystem NVARCHAR(100) NULL,
                         BatteryPercentage INT NULL
);

