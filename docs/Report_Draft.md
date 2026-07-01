# Local Supermarket Management System

## Cover page

Project title: Local Supermarket Management System for Small Shops
Student name: [Kapil Pooniya]
Module: [CST2550 Software Engineering Management and Development]
Date: [4 july 2026]

## Introduction

This project is a simple supermarket management system built with C# and .NET. It is designed for small shops to manage products, suppliers, stock, sales and reports. The system is beginner-friendly and uses a console menu to keep the interface easy to explain.

## System design

The system has three main parts:
- Models: represent products, suppliers, categories, stock, sales, and sale items.
- Services: provide basic operations for adding products, updating stock, recording sales, and generating reports.
- Data access: Entity Framework Core stores data in SQL Server LocalDB.

## Data structures and algorithms

This project uses custom data structures to show how data can be organized in memory.
- `ProductHashTable`: stores products by barcode and handles collisions with linked lists.
- `ProductBinarySearchTree`: stores products by title so the program can search by name.
- `SalesLinkedList`: stores recent sales in a simple linked list structure.

### Pseudocode example

Add product:
```
if product barcode exists then error
if price <= 0 then error
if quantity < 0 then error
save product to database
create stock record
```

Search by barcode:
```
index = hash(barcode)
traverse bucket list
return product or null
```

## Database design

The database uses tables for Categories, Suppliers, Products, Stock, Sales, and SaleItems. Relationships include:
- Supplier -> Products (one to many)
- Category -> Products (one to many)
- Product -> Stock (one to one)
- Sale -> SaleItems (one to many)

## Time complexity analysis

| Operation | Structure | Average | Worst case |
| --- | --- | --- | --- |
| Add product | Hash table | O(1) | O(n) |
| Search barcode | Hash table | O(1) | O(n) |
| Search name | BST | O(log n) | O(n) |
| Sale list add | Linked list | O(n) | O(n) |

## Testing approach

The application includes xUnit tests for:
- adding and validating products
- updating stock and detecting low stock
- searching products by barcode and name
- recording sales and rejecting invalid sales
- generating simple reports

## Limitations and reflection

The current system is a console app and does not have a graphical interface. It uses a simple LocalDB database and does not include advanced features like user authentication.

(Add your own reflection here on what was easy, what was hard, and how you improved the system.)

## Future improvements

- Add a graphical user interface
- Add editing for categories and suppliers
- Add sales history display and receipts export
- Add login and role-based access

## References

- Microsoft, Entity Framework Core documentation.
- xUnit.net documentation.
- SQL Server LocalDB tutorials.
