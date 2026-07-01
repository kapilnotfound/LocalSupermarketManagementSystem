using System;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data;
using SupermarketApp.Models;
using SupermarketApp.Services;
using Xunit;

namespace SupermarketTests
{
    public class StockTests
    {
        private SupermarketDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new SupermarketDbContext(options);
        }

        [Fact]
        public void UpdateStock_ChangesQuantity()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "111222333",
                title: "Tea",
                brand: "DrinkWell",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 2.50m,
                quantityInStock: 10);
            context.Products.Add(product);
            context.SaveChanges();

            var service = new StockService(context);
            service.UpdateStock(product.ProductId, 3);
            context.SaveChanges();

            var stock = context.Stock.FirstOrDefaultAsync(s => s.ProductId == product.ProductId).Result;
            Assert.Equal(3, stock.QuantityAvailable);
        }

        [Fact]
        public void CheckLowStock_ReturnsTrueWhenBelowThreshold()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "444555666",
                title: "Pasta",
                brand: "PantryPlus",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 1.50m,
                quantityInStock: 2);
            context.Products.Add(product);
            context.SaveChanges();
            var stock = TestDataFactory.CreateStock(product.ProductId, 2, 5);
            context.Stock.Add(stock);
            context.SaveChanges();

            var service = new StockService(context);
            Assert.True(service.CheckLowStock(product.ProductId));
        }

        [Fact]
        public void UpdateStock_RejectsNegativeStockUpdate()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "777888999",
                title: "Apples",
                brand: "FreshFarm",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 1.00m,
                quantityInStock: 5);
            context.Products.Add(product);
            context.SaveChanges();

            var service = new StockService(context);
            Assert.Throws<ArgumentException>(() => service.UpdateStock(product.ProductId, -1));
        }
    }
}
