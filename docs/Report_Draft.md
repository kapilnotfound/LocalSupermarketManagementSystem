# Local Supermarket Management System for Small Shops

## Cover Page

**Project Title:** Local Supermarket Management System for Small Shops  
**Student Name:** Kapil Pooniya  
**Module:** CST2550 Software Engineering Management and Development  
**Date:** 4 July 2026  

---

## 1. Introduction

This project is a C# .NET console-based Local Supermarket Management System designed for small shops. Small shops often manage stock, suppliers, and sales manually, which can cause mistakes such as incorrect stock levels, difficulty finding products, and poor tracking of sales. This system helps solve those problems by allowing shop staff to manage products, suppliers, stock updates, sales, and reports from a simple console menu.

The system uses SQL Server LocalDB with Entity Framework Core for database storage. It also includes custom data structures, such as a hash table, binary search tree, and linked list, to demonstrate algorithmic understanding. Unit tests are included using xUnit to check important features such as product validation, stock updates, sales recording, and reporting.

---

## 2. System Overview

The application provides a menu-driven interface with 14 options. Users can add products, update products, remove products, add suppliers, view suppliers, update stock, search products, record sales, and generate reports.

The main features are:

- Add, update, and remove products
- Add and view suppliers
- Update stock quantity
- Search products by barcode
- Search products by product name
- Record sales
- Automatically reduce stock after sales
- View low-stock products
- View products by category
- View sales by product
- View supplier stock list

The system is beginner-friendly because it uses a console menu instead of a complex graphical interface. This makes it easy to test, demonstrate, and explain during the coursework video.

---

## 3. System Design

The project is organised into clear folders:

- **Models:** represent database entities such as Product, Supplier, Category, Stock, Sale, and SaleItem.
- **Services:** contain the main business logic for products, suppliers, stock, sales, searching, and reports.
- **Data:** contains the Entity Framework database context.
- **DataStructures:** contains the custom hash table, binary search tree, and linked list.
- **SupermarketTests:** contains xUnit tests for checking functionality.

This separation makes the project easier to understand because each part has a specific responsibility.

---

## 4. Database Design

The system uses SQL Server LocalDB. Entity Framework Core is used to connect the C# application to the database.

The main database tables are:

- **Categories**
- **Suppliers**
- **Products**
- **Stock**
- **Sales**
- **SaleItems**

The relationships are:

- One supplier can have many products.
- One category can have many products.
- One product has one stock record.
- One sale can contain many sale items.
- One product can appear in many sale items.

This design helps keep the data organised and avoids storing repeated information unnecessarily.

---

## 5. Data Structures and Algorithms

The project includes custom data structures to demonstrate how data can be organised and searched efficiently.

### ProductHashTable

`ProductHashTable` is used for barcode-based searching. The barcode is used as the key, which allows fast lookup of products.

The hash table handles collisions using linked list chaining. If two barcodes generate the same index, they are stored in the same bucket and searched using traversal.

### ProductBinarySearchTree

`ProductBinarySearchTree` is used for product-name searching. Products can be inserted and searched based on their title.

In average cases, searching in a binary search tree is faster than a simple linear search. However, if the tree becomes unbalanced, the worst case can become slower.

### SalesLinkedList

`SalesLinkedList` is used to store and display sales records. New sales can be added to the list, and records can be displayed by traversing the linked list.

---

## 6. Pseudocode

### Add Product

```text
START
Input product details
Check if barcode already exists
IF barcode exists THEN
    Show error message
ELSE IF price is less than or equal to 0 THEN
    Show validation error
ELSE IF quantity is less than 0 THEN
    Show validation error
ELSE
    Save product to database
    Create stock record
    Show success message
END IF
END
```

### Search Product by Barcode

```text
START
Input barcode
Calculate hash index from barcode
Search linked list at that index
IF barcode is found THEN
    Display product details
ELSE
    Display product not found
END IF
END
```

### Record Sale

```text
START
Input product ID and quantity sold
Find product in database
Check current stock quantity
IF product does not exist THEN
    Show product not found
ELSE IF quantity sold is greater than stock THEN
    Show insufficient stock message
ELSE
    Create sale record
    Create sale item record
    Reduce product stock
    Save changes to database
    Show sale recorded successfully
END IF
END
```

---

## 7. Time Complexity Analysis

| Operation | Data Structure | Average Time Complexity | Worst Case Time Complexity |
| --- | --- | --- | --- |
| Add product by barcode | Hash table | O(1) | O(n) |
| Search product by barcode | Hash table | O(1) | O(n) |
| Remove product by barcode | Hash table | O(1) | O(n) |
| Search product by name | Binary search tree | O(log n) | O(n) |
| Add sale record | Linked list | O(n) | O(n) |
| Display sale records | Linked list | O(n) | O(n) |
| Database query | Entity Framework | Depends on query/indexing | O(n) |

The hash table is useful for fast barcode search. The binary search tree helps demonstrate sorted searching by product name. The linked list is useful for storing and displaying sale records.

---

## 8. Testing Approach

The application includes xUnit tests to check important features. The test project is stored in:

```text
src/SupermarketTests
```

The tests cover:

- Product validation
- Adding products
- Searching products by barcode
- Searching products by name
- Updating stock
- Detecting low stock
- Recording sales
- Rejecting invalid sales
- Generating reports

The final test result was:

```text
Test summary: total: 15, failed: 0, succeeded: 15, skipped: 0
```

This shows that the main tested features are working successfully.

---

## 9. Implementation and Debugging

During development, several issues were found and fixed. One issue was that the application could not run because `appsettings.json` was missing. This was fixed by adding the configuration file with the SQL Server LocalDB connection string.

Another issue happened when the application executable was locked because the app was still running in the background. This was fixed by closing the running process before building again.

A further issue occurred when searching products by name using Entity Framework. The query used `StringComparison.OrdinalIgnoreCase`, which Entity Framework could not translate into SQL. This was fixed by changing the search logic to use a database-compatible case-insensitive search method.

After these fixes, the project built successfully, all 15 unit tests passed, and the application ran correctly.

---

## 10. Screenshots and Evidence

Screenshots were added in the following folder:

```text
docs/Screenshots/
```

The screenshots show:

- Main menu
- Add product success
- Search product by barcode
- Search product by name
- Update stock
- Low-stock report
- Record sale
- Sales by product
- Supplier stock list
- Exit screen

These screenshots provide evidence that the application runs and that the main menu options work.

---

## 11. Limitations

The current system works as a console application, so it does not have a graphical user interface. It is suitable for coursework demonstration and small-shop management, but it is not yet a full commercial system.

Other limitations include:

- No user login system
- No role-based access control
- No barcode scanner hardware integration
- No receipt printing
- Reports are displayed in the console instead of being exported
- The binary search tree is mainly used to demonstrate data structure knowledge

---

## 12. Future Improvements

The system could be improved by adding:

- A graphical user interface
- User login and role-based access
- Barcode scanner support
- Receipt printing
- Customer accounts
- Advanced sales analytics
- Export reports to PDF or Excel
- Better validation and error messages
- Category management from the console menu

These improvements would make the system more suitable for real business use.

---

## 13. Reflection

This project helped me understand how to build a complete C# .NET application using models, services, database access, and unit testing. I also learned how to connect a console application to SQL Server LocalDB using Entity Framework Core.

One of the challenging parts was debugging errors related to database configuration and Entity Framework queries. I learned that some C# methods cannot be translated directly into SQL, so queries need to be written in a way that Entity Framework supports.

I also improved my understanding of Git and GitHub by committing changes, pushing updates, fixing rejected pushes, and keeping the repository clean. Overall, this project improved my confidence in building, testing, debugging, and documenting a software system.

---

## 14. Conclusion

The Local Supermarket Management System successfully provides key features needed by a small shop, including product management, supplier management, stock updates, searching, sales recording, and reports. The system uses SQL Server LocalDB for storage, Entity Framework Core for data access, custom data structures for algorithm demonstration, and xUnit tests for verification.

The project builds successfully, all 15 tests pass, and the application runs through a console menu. This shows that the final system meets the main coursework requirements.

---

## References

Microsoft. (2026) *Entity Framework Core documentation*. Microsoft Learn.

Microsoft. (2026) *.NET documentation*. Microsoft Learn.

xUnit.net. (2026) *xUnit testing framework documentation*.

Microsoft. (2026) *SQL Server LocalDB documentation*. Microsoft Learn.
