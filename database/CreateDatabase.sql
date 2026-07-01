CREATE DATABASE LocalSupermarketDb;
GO
USE LocalSupermarketDb;
GO

CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(250)
);

CREATE TABLE Suppliers (
    SupplierId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(150) NOT NULL,
    ContactPerson NVARCHAR(100),
    Phone NVARCHAR(50),
    Email NVARCHAR(100),
    Address NVARCHAR(250)
);

CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Barcode NVARCHAR(50) NOT NULL UNIQUE,
    Brand NVARCHAR(100),
    SupplierId INT NOT NULL,
    CategoryId INT NOT NULL,
    ExpiryDate DATE NULL,
    RestockDate DATE NULL,
    StockAvailabilityStatus NVARCHAR(50),
    Price DECIMAL(18,2) NOT NULL CHECK (Price > 0),
    QuantityInStock INT NOT NULL CHECK (QuantityInStock >= 0),
    CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE TABLE Stock (
    StockId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    QuantityAvailable INT NOT NULL CHECK (QuantityAvailable >= 0),
    LowStockThreshold INT NOT NULL CHECK (LowStockThreshold >= 0),
    LastUpdated DATETIME NOT NULL,
    CONSTRAINT FK_Stock_Product FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Sales (
    SaleId INT IDENTITY(1,1) PRIMARY KEY,
    SaleDate DATETIME NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL
);

CREATE TABLE SaleItems (
    SaleItemId INT IDENTITY(1,1) PRIMARY KEY,
    SaleId INT NOT NULL,
    ProductId INT NOT NULL,
    QuantitySold INT NOT NULL CHECK (QuantitySold > 0),
    UnitPrice DECIMAL(18,2) NOT NULL,
    LineTotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_SaleItem_Sale FOREIGN KEY (SaleId) REFERENCES Sales(SaleId),
    CONSTRAINT FK_SaleItem_Product FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
