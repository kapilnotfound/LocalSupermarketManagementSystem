# Local Supermarket Management System

## Project Overview

The Local Supermarket Management System is a C# .NET console application designed for small shops and local supermarkets. The system helps manage products, suppliers, stock levels, product searching, sales recording, and report generation.

The application uses SQL Server LocalDB with Entity Framework Core for data storage. It also includes custom data structures and xUnit tests to demonstrate the use of algorithms, data management, and software testing.

## Features

- Add new products
- Update existing product details
- Remove products
- Add suppliers
- View suppliers
- Update stock quantity
- Search products by barcode
- Search products by product name
- Record sales
- Automatically reduce stock after a sale
- View low-stock products
- View products by category
- View sales by product
- View supplier stock list
- Store data using SQL Server LocalDB
- Unit testing using xUnit

## Console Menu Options

When the application runs, the following menu is displayed:

```text
1. Add Product
2. Update Product
3. Remove Product
4. Add Supplier
5. View Suppliers
6. Update Stock
7. Search Product by Barcode
8. Search Product by Name
9. Record Sale
10. View Low Stock Report
11. View Products by Category
12. View Sales by Product
13. View Supplier Stock List
14. Exit
```

## Technologies Used

- C#
- .NET SDK 10.0
- SQL Server LocalDB
- Entity Framework Core
- xUnit
- Visual Studio Code
- Git and GitHub

## Custom Data Structures Used

The project includes custom data structures to support searching and reporting features.

### ProductHashTable

`ProductHashTable` is used for barcode-based product lookup.

- Barcode searching is fast because the barcode is used as the key.
- Average search time is `O(1)`.
- Worst-case search time is `O(n)` if collisions occur.

### ProductBinarySearchTree

`ProductBinarySearchTree` is used for product-name searching.

- Products can be inserted and searched using their names.
- Average search time is `O(log n)`.
- Worst-case search time is `O(n)` if the tree becomes unbalanced.

### SalesLinkedList

`SalesLinkedList` is used for managing and displaying sales records.

- New sale records can be added to the list.
- Displaying records requires traversal.
- Time complexity for display is `O(n)`.

## Algorithms and Complexity

| Data Structure | Operation | Average Time Complexity | Worst Time Complexity |
| --- | --- | --- | --- |
| ProductHashTable | Add/Search/Remove by barcode | O(1) | O(n) |
| ProductBinarySearchTree | Insert/Search by name | O(log n) | O(n) |
| SalesLinkedList | Add/Display sales | O(n) | O(n) |
| Database Search | Query records using Entity Framework | Depends on query/indexing | O(n) |

## Software Requirements

To run this project, the following software is required:

- Windows operating system
- .NET SDK 10.0 or later
- Visual Studio Code or Visual Studio
- SQL Server LocalDB
- Git

## Folder Structure

```text
LocalSupermarketManagementSystem/
├── database/
│   ├── CreateDatabase.sql
│   └── SeedData.sql
├── docs/
│   ├── Report_Draft.md
│   └── Screenshots/
├── src/
│   ├── SupermarketApp/
│   │   ├── Data/
│   │   ├── DataStructures/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── SupermarketApp.csproj
│   └── SupermarketTests/
├── README.md
└── LocalSupermarketManagementSystem.slnx
```

## Database Setup

The application uses SQL Server LocalDB. The connection string is stored in:

```text
src/SupermarketApp/appsettings.json
```

The application uses Entity Framework Core `EnsureCreated()` to create the database tables automatically if they do not already exist.

The database scripts are also included in the `database` folder.

To create and seed the database manually, run these commands from the project root:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\CreateDatabase.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\SeedData.sql
```

## How to Build the Project

Open the project root folder in VS Code or Visual Studio.

Run this command from the project root:

```powershell
dotnet build
```

Expected result:

```text
Build succeeded
```

## How to Run the Application

Run this command from the project root:

```powershell
dotnet run --project src/SupermarketApp
```

The console menu will open. Users can then choose options from 1 to 14 to manage products, suppliers, stock, sales, and reports.

## How to Run Tests

Run this command from the project root:

```powershell
dotnet test
```

Current test result:

```text
Test summary: total: 15, failed: 0, succeeded: 15, skipped: 0
```

## Testing Evidence

The project includes unit tests in:

```text
src/SupermarketTests
```

The tests cover product, stock, sales, and report-related functionality.

The current test result is:

```text
15 tests passed
0 tests failed
```

## Screenshots

Application screenshots are stored in:

```text
docs/Screenshots/
```

The screenshots show the following application features:

- Main menu
- Add product
- Search product by barcode
- Search product by name
- Update stock
- Low-stock report
- Record sale
- Sales by product
- Supplier stock list
- Exit screen

## Database Files

The database folder contains:

```text
database/CreateDatabase.sql
database/SeedData.sql
```

`CreateDatabase.sql` is used to create the database structure.

`SeedData.sql` is used to insert sample data for testing and demonstration.

## Example App Workflow

A typical use of the application is:

1. Add a supplier.
2. Add a product linked to a supplier and category.
3. Search the product by barcode.
4. Search the product by name.
5. Update the product stock quantity.
6. Record a sale.
7. View the low-stock report.
8. View sales by product.
9. View supplier stock list.

## GitHub Repository

Repository link:

```text
https://github.com/kapilnotfound/LocalSupermarketManagementSystem
```

## Limitations

- The system is a console application and does not include a graphical user interface.
- It is designed for small-shop coursework demonstration.
- Barcode scanner hardware is not integrated.
- Reports are displayed in the console and are not exported to PDF or Excel.
- The binary search tree is mainly used to demonstrate data structure knowledge.

## Future Improvements

Possible future improvements include:

- Add a graphical user interface
- Add barcode scanner support
- Add receipt printing
- Add customer accounts
- Add advanced sales analytics
- Add report export to PDF or Excel
- Add user login and role-based access
- Improve validation and error messages

## Conclusion

This project demonstrates a working local supermarket management system using C# .NET, SQL Server LocalDB, Entity Framework Core, custom data structures, and unit testing. It supports key small-shop operations such as product management, supplier management, stock updates, searching, sales recording, and report generation.
