using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data;
using SupermarketApp.Models;
using SupermarketApp.Services;
using Xunit;

namespace SupermarketTests
{
    public class SalesTests
    {
        private SupermarketDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new SupermarketDbContext(options);
        }

        [Fact]
        public void RecordSale_SuccessfullyRecordsSaleAndReducesStock()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "101010101",
                title: "Chicken",
                brand: "FreshFarm",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 5.00m,
                quantityInStock: 10);
            context.Products.Add(product);
            context.SaveChanges();
            context.Stock.Add(TestDataFactory.CreateStock(product.ProductId, 10, 2));
            context.SaveChanges();

            var stockService = new StockService(context);
            var salesService = new SalesService(context, stockService);
            var result = salesService.RecordSale(new List<(string barcode, int quantity)> { ("101010101", 3) });

            Assert.True(result.Success);
            Assert.Contains("Chicken x3", result.Receipt);
            Assert.Equal(7, context.Products.Find(product.ProductId).QuantityInStock);
        }

        [Fact]
        public void RecordSale_RejectsWhenStockIsNotEnough()
        {
            using var context = CreateInMemoryContext();
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "202020202",
                title: "Honey",
                brand: "SweetBrand",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 6.00m,
                quantityInStock: 2);
            context.Products.Add(product);
            context.SaveChanges();
            context.Stock.Add(TestDataFactory.CreateStock(product.ProductId, 2, 1));
            context.SaveChanges();

            var stockService = new StockService(context);
            var salesService = new SalesService(context, stockService);
            var result = salesService.RecordSale(new List<(string barcode, int quantity)> { ("202020202", 5) });

            Assert.False(result.Success);
            Assert.Contains("Not enough stock", result.Message);
        }
    }
}
