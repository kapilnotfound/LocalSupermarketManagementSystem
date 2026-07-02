using SupermarketApp.Models;
using System;
using System.Collections.Generic;

namespace SupermarketTests
{
    /// <summary>
    /// Factory class for creating valid test objects with all required fields populated.
    /// Ensures consistent test data across all test files.
    /// </summary>
    public static class TestDataFactory
    {
        private static int _categoryCounter = 1;
        private static int _supplierCounter = 1;
        private static int _productCounter = 1;
        private static int _saleCounter = 1;

        /// <summary>
        /// Creates a valid Category with all required fields.
        /// </summary>
        public static Category CreateCategory(string categoryName = null, string description = null)
        {
            return new Category
            {
                CategoryId = _categoryCounter++,
                CategoryName = categoryName ?? $"Category {_categoryCounter}",
                Description = description ?? $"Description for Category {_categoryCounter}",
                Products = new List<Product>()
            };
        }

        /// <summary>
        /// Creates a valid Supplier with all required fields.
        /// </summary>
        public static Supplier CreateSupplier(string supplierName = null, string contactPerson = null)
        {
            return new Supplier
            {
                SupplierId = _supplierCounter++,
                SupplierName = supplierName ?? $"Supplier {_supplierCounter}",
                ContactPerson = contactPerson ?? $"Contact {_supplierCounter}",
                Phone = "555-0100",
                Email = $"supplier{_supplierCounter}@example.com",
                Address = $"123 Supplier St, City {_supplierCounter}",
                Products = new List<Product>()
            };
        }

        /// <summary>
        /// Creates a valid Product with all required fields.
        /// </summary>
        public static Product CreateProduct(
            string title = null,
            string barcode = null,
            string brand = null,
            decimal price = 10.00m,
            int quantityInStock = 100,
            int supplierId = 1,
            int categoryId = 1)
        {
            var productId = _productCounter++;
            
            return new Product
            {
                ProductId = productId,
                Title = title ?? $"Product {productId}",
                Barcode = barcode ?? $"BC{productId:D6}",
                Brand = brand ?? $"Brand {productId}",
                Price = price,
                QuantityInStock = quantityInStock,
                SupplierId = supplierId,
                CategoryId = categoryId,
                ExpiryDate = DateTime.Now.AddMonths(6),
                RestockDate = DateTime.Now.AddMonths(1),
                StockAvailabilityStatus = "In Stock",
                SaleItems = new List<SaleItem>()
            };
        }

        /// <summary>
        /// Creates a valid Stock record with all required fields.
        /// </summary>
        public static Stock CreateStock(
            int productId,
            int quantityAvailable = 20,
            int lowStockThreshold = 5)
        {
            return new Stock
            {
                ProductId = productId,
                QuantityAvailable = quantityAvailable,
                LowStockThreshold = lowStockThreshold,
                LastUpdated = DateTime.Now
            };
        }

        /// <summary>
        /// Creates a valid Sale record with all required fields.
        /// </summary>
        public static Sale CreateSale(params SaleItem[] saleItems)
        {
            var items = new List<SaleItem>(saleItems ?? new SaleItem[0]);
            decimal totalAmount = 0m;
            foreach (var item in items)
            {
                totalAmount += item.LineTotal;
            }

            return new Sale
            {
                SaleId = _saleCounter++,
                SaleDate = DateTime.Now,
                TotalAmount = totalAmount > 0 ? totalAmount : 50.00m,
                SaleItems = items
            };
        }

        /// <summary>
        /// Creates a valid SaleItem with all required fields.
        /// </summary>
        public static SaleItem CreateSaleItem(
            int productId,
            int quantitySold = 5,
            decimal unitPrice = 10.00m)
        {
            var lineTotal = quantitySold * unitPrice;
            
            return new SaleItem
            {
                ProductId = productId,
                QuantitySold = quantitySold,
                UnitPrice = unitPrice,
                LineTotal = lineTotal
            };
        }

        /// <summary>
        /// Resets factory counters (useful for test isolation).
        /// </summary>
        public static void Reset()
        {
            _categoryCounter = 1;
            _supplierCounter = 1;
            _productCounter = 1;
            _saleCounter = 1;
        }
    }
}
