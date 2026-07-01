using System;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data;
using SupermarketApp.Models;
using SupermarketApp.Services;
using Xunit;

namespace SupermarketTests
{
    public class ProductTests
    {
        private SupermarketDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new SupermarketDbContext(options);
        }

        [Fact]
        public void AddProduct_SuccessfullyAddsProduct()
        {
            using var context = CreateInMemoryContext();
            var service = new ProductService(context);
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "000111222",
                title: "Milk",
                brand: "FarmFresh",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 2.50m,
                quantityInStock: 20);

            service.AddProduct(product);
            Assert.NotNull(context.Products.FirstOrDefaultAsync(p => p.Barcode == "000111222").Result);
        }

        [Fact]
        public void AddProduct_RejectsDuplicateBarcode()
        {
            using var context = CreateInMemoryContext();
            var service = new ProductService(context);
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product1 = TestDataFactory.CreateProduct(
                barcode: "123456789",
                title: "Bread",
                brand: "BakeHouse",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 1.75m,
                quantityInStock: 10);
            var product2 = TestDataFactory.CreateProduct(
                barcode: "123456789",
                title: "Bread Two",
                brand: "BakeHouse",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 2.00m,
                quantityInStock: 5);

            service.AddProduct(product1);
            Assert.Throws<InvalidOperationException>(() => service.AddProduct(product2));
        }

        [Fact]
        public void AddProduct_RejectsNegativePrice()
        {
            using var context = CreateInMemoryContext();
            var service = new ProductService(context);
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "987654321",
                title: "Eggs",
                brand: "FarmFresh",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: -1.00m,
                quantityInStock: 10);

            Assert.Throws<ArgumentException>(() => service.AddProduct(product));
        }

        [Fact]
        public void AddProduct_RejectsNegativeQuantity()
        {
            using var context = CreateInMemoryContext();
            var service = new ProductService(context);
            var supplier = TestDataFactory.CreateSupplier();
            var category = TestDataFactory.CreateCategory();
            context.Suppliers.Add(supplier);
            context.Categories.Add(category);
            context.SaveChanges();

            var product = TestDataFactory.CreateProduct(
                barcode: "555666777",
                title: "Rice",
                brand: "PantryPlus",
                supplierId: supplier.SupplierId,
                categoryId: category.CategoryId,
                price: 3.50m,
                quantityInStock: -5);

            Assert.Throws<ArgumentException>(() => service.AddProduct(product));
        }
    }
}
