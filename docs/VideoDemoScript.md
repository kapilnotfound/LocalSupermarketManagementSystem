# Video Demo Script

## Opening
Hello, I am [Your Name] and this is a demo of the Local Supermarket Management System. In this video, I will show how the console app works and how the database stores supermarket data.

## App running
1. Start the console application in VS Code.
2. The menu appears with options like Add Product, Update Product, Record Sale, and reports.
3. Explain that this is a beginner-friendly app built with .NET, Entity Framework, and LocalDB.

## Product management
1. Choose Add Product.
2. Enter a product name such as "Milk", barcode, supplier ID, category ID, price, and quantity.
3. Show success message.
4. Choose Update Product and change the price or quantity.
5. Choose Remove Product to show simple deletion.

## Supplier management
1. Choose Add Supplier.
2. Enter supplier name, contact person, phone, email, and address.
3. Show View Suppliers to list saved suppliers.

## Stock update
1. Choose Update Stock.
2. Enter the product barcode and a new quantity.
3. Show that the stock record and product quantity are updated.

## Barcode search
1. Choose Search Product by Barcode.
2. Enter a barcode and show the found product details.

## Name search
1. Choose Search Product by Name.
2. Enter a product title and show the found result.

## Sale recording
1. Choose Record Sale.
2. Enter a barcode and quantity for a small purchase.
3. Finish input and show the receipt summary.
4. Mention that the app checks stock and updates quantity.

## Low-stock report
1. Choose View Low Stock Report.
2. Show items that are below their threshold.
3. Explain why this helps a small shop reorder products.

## Products by category
1. Choose View Products by Category.
2. Show grouped product counts.
3. Explain how categories help organize the inventory.

## Sales report
1. Choose View Sales by Product.
2. Show quantities sold and total sales values.
3. Explain how this gives a simple sales summary.

## SQL Server tables
1. Open SQL Server Object Explorer or use the database scripts.
2. Show the CreateDatabase.sql and SeedData.sql files.
3. Explain how the database tables map to the models in the app.

## Unit tests running
1. Open the Test Explorer in VS Code or use `dotnet test`.
2. Run the xUnit tests and show the result.
3. Mention tests cover product validation, stock, searches, sales, and reports.

## Closing
Thank you for watching. This project is simple to explain and is a good example of using C# and SQL Server for a small supermarket system.
