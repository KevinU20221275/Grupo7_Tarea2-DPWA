CREATE DATABASE AlcaldiaDb;
GO
USE AlcaldiaDb;
GO

-- Table for Residents
CREATE TABLE Residents (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    FirstName VARCHAR(75) NOT NULL,
    LastName VARCHAR(75) NOT NULL,
    Address VARCHAR(250),
    BirthDate DATETIME
);

-- Table for Positions
CREATE TABLE Positions (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Position VARCHAR(100) NOT NULL,
    Description VARCHAR(250)
);

-- Table for Municipal Employees
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    FirstName VARCHAR(75) NOT NULL,
    LastName VARCHAR(75) NOT NULL,
    PositionId INT NOT NULL,
    Salary DECIMAL(10, 2) NOT NULL,
    CONSTRAINT Fk_Position FOREIGN KEY (PositionId) REFERENCES Positions(Id)
);

-- Table for Municipal Services
CREATE TABLE MunicipalServices (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Service VARCHAR(150) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Cost DECIMAL(12,2) NOT NULL
);

-- Table for Service Requests
CREATE TABLE ServiceRequests (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ResidentId INT NOT NULL,
    ServiceId INT NOT NULL,
    RequestDate DATETIME NOT NULL,
    Status BIT NOT NULL,
    CONSTRAINT Fk_Resident FOREIGN KEY (ResidentId) REFERENCES Residents(Id),
    CONSTRAINT FK_Service FOREIGN KEY (ServiceId) REFERENCES MunicipalServices(Id)
);

SELECT Id, Position, Description FROM Positions WHERE Id=2;