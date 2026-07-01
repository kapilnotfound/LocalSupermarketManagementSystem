using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data;
using SupermarketApp.Models;
using SupermarketApp.Services;

namespace SupermarketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration from the folder where the app is running
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SupermarketDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var context = new SupermarketDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            var productService = new ProductService(context);
            var supplierService = new SupplierService(context);
            var stockService = new StockService(context);
            var salesService = new SalesService(context, stockService);
            var reportService = new ReportService(context);

            while (true)
            {
                Console.WriteLine("\n=== Local Supermarket Management System ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Remove Product");
                Console.WriteLine("4. Add Supplier");
                Console.WriteLine("5. View Suppliers");
                Console.WriteLine("6. Update Stock");
                Console.WriteLine("7. Search Product by Barcode");
                Console.WriteLine("8. Search Product by Name");
                Console.WriteLine("9. Record Sale");
                Console.WriteLine("10. View Low Stock Report");
                Console.WriteLine("11. View Products by Category");
                Console.WriteLine("12. View Sales by Product");
                Console.WriteLine("13. View Supplier Stock List");
                Console.WriteLine("14. Exit");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            AddProduct(productService, context);
                            break;
                        case "2":
                            UpdateProduct(productService, context);
                            break;
                        case "3":
                            RemoveProduct(productService);
                            break;
                        case "4":
                            AddSupplier(supplierService);
                            break;
                        case "5":
                            ViewSuppliers(supplierService);
                            break;
                        case "6":
                            UpdateStock(stockService, context);
                            break;
                        case "7":
                            SearchByBarcode(productService);
                            break;
                        case "8":
                            SearchByName(productService);
                            break;
                        case "9":
                            RecordSale(salesService, context);
                            break;
                        case "10":
                            reportService.PrintLowStockReport();
                            break;
                        case "11":
                            reportService.PrintProductsByCategory();
                            break;
                        case "12":
                            reportService.PrintSalesByProduct();
                            break;
                        case "13":
                            reportService.PrintSupplierStockList();
                            break;
                        case "14":
                            Console.WriteLine("Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Please enter a number from 1 to 14.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void AddProduct(ProductService productService, SupermarketDbContext context)
        {
            Console.WriteLine("-- Add New Product --");
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Barcode: ");
            var barcode = Console.ReadLine();
            Console.Write("Brand: ");
            var brand = Console.ReadLine();
            Console.Write("Supplier ID: ");
            var supplierId = int.TryParse(Console.ReadLine(), out var sid) ? sid : 0;
            Console.Write("Category ID: ");
            var categoryId = int.TryParse(Console.ReadLine(), out var cid) ? cid : 0;
            Console.Write("Expiry Date (yyyy-MM-dd): ");
            var expiryText = Console.ReadLine();
            Console.Write("Restock Date (yyyy-MM-dd): ");
            var restockText = Console.ReadLine();
            Console.Write("Price: ");
            var price = decimal.TryParse(Console.ReadLine(), out var p) ? p : -1;
            Console.Write("Quantity In Stock: ");
            var quantity = int.TryParse(Console.ReadLine(), out var q) ? q : -1;

            var product = new Product
            {
                Title = title,
                Barcode = barcode,
                Brand = brand,
                SupplierId = supplierId,
                CategoryId = categoryId,
                ExpiryDate = DateTime.TryParse(expiryText, out var expiry) ? expiry : DateTime.MinValue,
                RestockDate = DateTime.TryParse(restockText, out var restock) ? restock : DateTime.MinValue,
                Price = price,
                QuantityInStock = quantity,
                StockAvailabilityStatus = quantity <= 0 ? "Out of stock" : "In stock"
            };

            productService.AddProduct(product);
            Console.WriteLine("Product added successfully.");
        }

        private static void UpdateProduct(ProductService productService, SupermarketDbContext context)
        {
            Console.WriteLine("-- Update Product --");
            Console.Write("Barcode of product to update: ");
            var barcode = Console.ReadLine();
            var existing = productService.SearchByBarcode(barcode);
            if (existing == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("New Title (leave blank to keep): ");
            var title = Console.ReadLine();
            Console.Write("New Price: ");
            var price = decimal.TryParse(Console.ReadLine(), out var p) ? p : existing.Price;
            Console.Write("New Quantity In Stock: ");
            var quantity = int.TryParse(Console.ReadLine(), out var q) ? q : existing.QuantityInStock;

            if (!string.IsNullOrWhiteSpace(title)) existing.Title = title;
            existing.Price = price;
            existing.QuantityInStock = quantity;
            existing.StockAvailabilityStatus = quantity <= 0 ? "Out of stock" : "In stock";

            productService.UpdateProduct(existing);
            Console.WriteLine("Product updated.");
        }

        private static void RemoveProduct(ProductService productService)
        {
            Console.WriteLine("-- Remove Product --");
            Console.Write("Barcode: ");
            var barcode = Console.ReadLine();
            productService.RemoveProduct(barcode);
            Console.WriteLine("Product removed if it existed.");
        }

        private static void AddSupplier(SupplierService supplierService)
        {
            Console.WriteLine("-- Add Supplier --");
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Contact Person: ");
            var contact = Console.ReadLine();
            Console.Write("Phone: ");
            var phone = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Address: ");
            var address = Console.ReadLine();

            var supplier = new Supplier
            {
                SupplierName = name,
                ContactPerson = contact,
                Phone = phone,
                Email = email,
                Address = address
            };

            supplierService.AddSupplier(supplier);
            Console.WriteLine("Supplier added successfully.");
        }

        private static void ViewSuppliers(SupplierService supplierService)
        {
            Console.WriteLine("-- Supplier List --");
            var suppliers = supplierService.GetSuppliers();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Name: {supplier.SupplierName}, Contact: {supplier.ContactPerson}");
            }
        }

        private static void UpdateStock(StockService stockService, SupermarketDbContext context)
        {
            Console.WriteLine("-- Update Stock --");
            Console.Write("Product Barcode: ");
            var barcode = Console.ReadLine();
            var product = context.Products.FirstOrDefault(p => p.Barcode == barcode);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("New quantity available: ");
            var quantity = int.TryParse(Console.ReadLine(), out var q) ? q : -1;
            stockService.UpdateStock(product.ProductId, quantity);
            context.SaveChanges();
            Console.WriteLine("Stock updated.");
        }

        private static void SearchByBarcode(ProductService productService)
        {
            Console.WriteLine("-- Search by Barcode --");
            Console.Write("Barcode: ");
            var barcode = Console.ReadLine();
            var product = productService.SearchByBarcode(barcode);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }
            Console.WriteLine($"Found: {product.Title} | Brand: {product.Brand} | Price: {product.Price} | Quantity: {product.QuantityInStock}");
        }

        private static void SearchByName(ProductService productService)
        {
            Console.WriteLine("-- Search by Name --");
            Console.Write("Product name: ");
            var name = Console.ReadLine();
            var product = productService.SearchByName(name);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }
            Console.WriteLine($"Found: {product.Title} | Barcode: {product.Barcode} | Price: {product.Price} | Quantity: {product.QuantityInStock}");
        }

        private static void RecordSale(SalesService salesService, SupermarketDbContext context)
        {
            Console.WriteLine("-- Record Sale --");
            var items = new List<(string barcode, int quantity)>();
            while (true)
            {
                Console.Write("Product barcode (leave blank to finish): ");
                var barcode = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(barcode)) break;
                Console.Write("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out var quantity))
                {
                    Console.WriteLine("Invalid quantity, try again.");
                    continue;
                }
                items.Add((barcode, quantity));
            }

            if (items.Count == 0)
            {
                Console.WriteLine("No items added to the sale.");
                return;
            }

            var result = salesService.RecordSale(items);
            if (!result.Success)
            {
                Console.WriteLine($"Sale failed: {result.Message}");
                return;
            }

            Console.WriteLine(result.Receipt);
        }
    }
}
