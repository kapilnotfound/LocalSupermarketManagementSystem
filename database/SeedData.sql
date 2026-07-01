USE LocalSupermarketDb;
GO

INSERT INTO Categories (CategoryName, Description) VALUES
('Dairy', 'Milk, cheese and dairy products'),
('Bakery', 'Bread and baked goods'),
('Pantry', 'Rice, pasta and dry food'),
('Beverages', 'Drinks, tea and coffee'),
('Health', 'Personal care and hygiene');

INSERT INTO Suppliers (SupplierName, ContactPerson, Phone, Email, Address) VALUES
('FreshFarm Suppliers', 'Amit Patel', '0123456789', 'amit@freshfarm.com', '123 Market Street'),
('BakeHouse Ltd', 'Sara Khan', '0987654321', 'sara@bakehouse.com', '56 Baker Road'),
('PantryPlus', 'John Lee', '0111222333', 'john@pantryplus.com', '78 Grocery Lane'),
('DrinkWell Co', 'Nina Roy', '0222333444', 'nina@drinkwell.com', '90 Beverage Ave'),
('CleanCare', 'Rohan Singh', '0333444555', 'rohan@cleancare.com', '12 Health Blvd');

INSERT INTO Products (Title, Barcode, Brand, SupplierId, CategoryId, ExpiryDate, RestockDate, StockAvailabilityStatus, Price, QuantityInStock) VALUES
('Milk', '0001', 'FreshFarm', 1, 1, '2026-08-01', '2026-06-15', 'In stock', 1.25, 50),
('Bread', '0002', 'BakeHouse', 2, 2, '2026-06-08', '2026-06-10', 'In stock', 1.00, 40),
('Rice', '0003', 'PantryPlus', 3, 3, '2030-01-01', '2026-07-01', 'In stock', 8.00, 30),
('Eggs', '0004', 'FreshFarm', 1, 1, '2026-06-20', '2026-06-14', 'In stock', 2.20, 25),
('Tea', '0005', 'DrinkWell', 4, 4, '2030-12-31', '2026-07-05', 'In stock', 3.50, 20),
('Pasta', '0006', 'PantryPlus', 3, 3, '2027-05-01', '2026-06-18', 'In stock', 2.80, 15),
('Apples', '0007', 'FreshFarm', 1, 3, '2026-06-18', '2026-06-12', 'In stock', 1.50, 18),
('Chicken', '0008', 'FreshFarm', 1, 3, '2026-06-19', '2026-06-16', 'In stock', 6.75, 12),
('Shampoo', '0009', 'CleanCare', 5, 5, '2030-01-01', '2026-07-02', 'In stock', 4.90, 10),
('Cereal', '0010', 'PantryPlus', 3, 3, '2027-09-01', '2026-07-03', 'In stock', 4.00, 22);

INSERT INTO Stock (ProductId, QuantityAvailable, LowStockThreshold, LastUpdated) VALUES
(1, 50, 10, GETDATE()),
(2, 40, 10, GETDATE()),
(3, 30, 5, GETDATE()),
(4, 25, 8, GETDATE()),
(5, 20, 6, GETDATE()),
(6, 15, 5, GETDATE()),
(7, 18, 7, GETDATE()),
(8, 12, 5, GETDATE()),
(9, 10, 4, GETDATE()),
(10, 22, 6, GETDATE());

INSERT INTO Sales (SaleDate, TotalAmount) VALUES
('2026-06-10 10:15:00', 12.50),
('2026-06-11 14:30:00', 18.20),
('2026-06-12 09:00:00', 5.75);

INSERT INTO SaleItems (SaleId, ProductId, QuantitySold, UnitPrice, LineTotal) VALUES
(1, 1, 5, 1.25, 6.25),
(1, 2, 3, 1.00, 3.00),
(1, 4, 1, 2.20, 2.20),
(2, 3, 2, 8.00, 16.00),
(2, 5, 1, 3.50, 3.50),
(3, 10, 1, 4.00, 4.00),
(3, 9, 1, 4.90, 4.90);
