/**************************************************************************
 * Purchase Bill Management System — SQL Server Schema
 **************************************************************************/
IF DB_ID('PurchaseBillDb') IS NOT NULL
BEGIN
    ALTER DATABASE PurchaseBillDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PurchaseBillDb;
END
GO
CREATE DATABASE PurchaseBillDb;
GO
USE PurchaseBillDb;
GO
CREATE TABLE Location_Details (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Location_Code NVARCHAR(50)  NOT NULL,
    Location_Name NVARCHAR(200) NOT NULL,
    CreatedAt     DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt     DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_Location_Code UNIQUE (Location_Code)
);
GO
CREATE TABLE AppUsers (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    Email        NVARCHAR(200) NOT NULL UNIQUE,
    DisplayName  NVARCHAR(200) NOT NULL,
    LastLoginAt  DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    LoginCount   INT           NOT NULL DEFAULT 1,
    CreatedAt    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME()
);
GO
CREATE TABLE Purchase_Bills (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    ItemName       NVARCHAR(100)   NOT NULL,
    Batch          NVARCHAR(200)   NOT NULL,
    StandardCost   DECIMAL(18,2)   NOT NULL,
    StandardPrice  DECIMAL(18,2)   NOT NULL,
    DiscountPct    DECIMAL(5,2)    NOT NULL,
    Qty            INT             NOT NULL,
    TotalCost      DECIMAL(18,2)   NOT NULL,
    TotalSelling   DECIMAL(18,2)   NOT NULL,
    CreatedByEmail NVARCHAR(200)   NULL,
    CreatedAt      DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT CK_Qty_Positive    CHECK (Qty > 0),
    CONSTRAINT CK_Discount_Range  CHECK (DiscountPct >= 0 AND DiscountPct <= 100)
);
GO
INSERT INTO Location_Details (Location_Code, Location_Name) VALUES
('LOC01', 'Main Store'),
('LOC02', 'Warehouse A'),
('LOC03', 'Outlet B');
GO
