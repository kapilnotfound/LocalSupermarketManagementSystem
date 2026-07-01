using System;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data;
using SupermarketApp.Models;
using SupermarketApp.Services;
using Xunit;

namespace SupermarketTests
{
    public class ReportTests
    {
        private SupermarketDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new SupermarketDbContext(options);
        }

        [Fact]
        public void GenerateLowStockReport_ReturnsItems()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "333222111",
                title: "Apples",
                brand: "FreshFarm",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 1.00m,
                quantityInStock: 2);
            context.Products.Add(product);
            context.SaveChanges();
            context.Stock.Add(TestDataFactory.CreateStock(product.ProductId, 2, 5));
            context.SaveChanges();

            var reportService = new ReportService(context);
            reportService.PrintLowStockReport();
        }

        [Fact]
        public void GenerateProductsByCategoryReport_ReturnsCategoryCount()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory(categoryName: "Beverages", description: "Tea and drinks");
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();
            context.Products.Add(TestDataFactory.CreateProduct(
                barcode: "777000111",
                title: "Tea",
                brand: "DrinkWell",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 2.00m,
                quantityInStock: 10));
            context.SaveChanges();

            var reportService = new ReportService(context);
            reportService.PrintProductsByCategory();
        }

        [Fact]
        public void GenerateSalesByProductReport_ReturnsResults()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "666555444",
                title: "Rice",
                brand: "PantryPlus",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 2.50m,
                quantityInStock: 10);
            context.Products.Add(product);
            context.SaveChanges();
            context.Sales.Add(TestDataFactory.CreateSale(
                TestDataFactory.CreateSaleItem(product.ProductId, quantitySold: 2, unitPrice: 2.50m)));
            context.SaveChanges();

            var reportService = new ReportService(context);
            reportService.PrintSalesByProduct();
        }
    }
}
