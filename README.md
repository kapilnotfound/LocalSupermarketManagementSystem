# Local Supermarket Management System

## Project overview

This is a beginner-friendly C# .NET console application for managing a small supermarket. It uses SQL Server LocalDB, Entity Framework Core, and xUnit tests.

## Features

- Product management with add, update, and remove operations
- Supplier management and supplier listing
- Stock updates and low-stock checks
- Barcode and product name search
- Record sales with stock reduction
- Reports for low stock, sales by product, products by category, and supplier stock list
- Custom data structures: hash table, binary search tree, and linked list

## Software requirements

- Windows
- .NET SDK 10.0 or later
- Visual Studio Code
- SQL Server LocalDB

## Setup steps

1. Open the `LocalSupermarketManagementSystem` folder in VS Code.
2. Restore packages with `dotnet restore`.
3. Build the project with `dotnet build`.

## Database setup

The app uses LocalDB with the connection string in `src/SupermarketApp/appsettings.json`.

To create the database manually:

```powershell
sqlcmd -S (localdb)\MSSQLLocalDB -i database\CreateDatabase.sql
sqlcmd -S (localdb)\MSSQLLocalDB -i database\SeedData.sql
```

The app also calls `EnsureCreated()` so it can create tables automatically.

## How to run the app

From the `src/SupermarketApp` folder:

```powershell
dotnet run
```

Follow the console menu to add products, update stock, record sales, and view reports.

## How to run tests

From the `src/SupermarketTests` folder:

```powershell
dotnet test
```

## Data structures used

- `ProductHashTable`: custom hash table for barcode lookup
- `ProductBinarySearchTree`: custom binary search tree for product name search
- `SalesLinkedList`: custom linked list for sale history

## Algorithms and complexity

| Data structure | Operation | Average time | Worst time |
| --- | --- | --- | --- |
| ProductHashTable | Add/Search/Remove | O(1) | O(n) |
| ProductBinarySearchTree | Insert/Search | O(log n) | O(n) |
| SalesLinkedList | Add/Display | O(n) | O(n) |

## Folder structure

LocalSupermarketManagementSystem/
- `src/SupermarketApp` - console app code and EF context
- `src/SupermarketTests` - xUnit test project
- `database` - SQL creation and seed scripts
- `docs` - report draft and demo script
- `README.md` - project information

## GitHub submission

Use the following commands to prepare your repository:

```bash
git init
git add .
git commit -m "Initial supermarket system setup"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/LocalSupermarketManagementSystem.git
git push -u origin main
```

Replace `YOUR_USERNAME` with your GitHub username.
