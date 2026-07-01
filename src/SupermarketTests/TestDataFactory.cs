using System;
using SupermarketApp.Models;

namespace SupermarketTests
{
    public static class TestDataFactory
    {
        public static Product CreateProduct(
            string barcode,
            string title = "Sample Product",
            string brand = "GenericBrand",
            int supplierId = 1,
            int categoryId = 1,
            decimal price = 1.00m,
            int quantityInStock = 1)
        {
            return new Product
            {
                Title = title,
                Barcode = barcode,
                Brand = brand,
                SupplierId = supplierId,
                CategoryId = categoryId,
                ExpiryDate = DateTime.Now.AddMonths(6),
                RestockDate = DateTime.Now.AddDays(7),
                StockAvailabilityStatus = quantityInStock <= 0 ? "Out of stock" : "In stock",
                Price = price,
                QuantityInStock = quantityInStock
            };
        }

        public static Supplier CreateSupplier(
            string name = "Default Supplier",
            string contactPerson = "Supplier Contact",
            string phone = "0000000000",
            string email = "supplier@example.com",
            string address = "123 Supplier Street")
        {
            return new Supplier
            {
                SupplierName = name,
                ContactPerson = contactPerson,
                Phone = phone,
                Email = email,
                Address = address
            };
        }

        public static Category CreateCategory(
            string categoryName = "General",
            string description = "General category description")
        {
            return new Category
            {
                CategoryName = categoryName,
                Description = description
            };
        }

        public static Stock CreateStock(
            int productId,
            int quantityAvailable = 10,
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

        public static Sale CreateSale(params SaleItem[] items)
        {
            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                TotalAmount = 0m
            };

            foreach (var item in items)
            {
                sale.SaleItems.Add(item);
                sale.TotalAmount += item.LineTotal;
            }

            return sale;
        }

        public static SaleItem CreateSaleItem(
            int productId,
            int quantitySold = 1,
            decimal unitPrice = 1.00m)
        {
            return new SaleItem
            {
                ProductId = productId,
                QuantitySold = quantitySold,
                UnitPrice = unitPrice,
                LineTotal = quantitySold * unitPrice
            };
        }
    }
}
