using System;
using System.Collections.Generic;
using System.Linq;
using SupermarketApp.Data;
using SupermarketApp.DataStructures;
using SupermarketApp.Models;

namespace SupermarketApp.Services
{
    public class ProductService
    {
        private readonly SupermarketDbContext _context;
        private readonly ProductHashTable _barcodeIndex;
        private readonly ProductBinarySearchTree _nameIndex;

        public ProductService(SupermarketDbContext context)
        {
            _context = context;
            _barcodeIndex = new ProductHashTable();
            _nameIndex = new ProductBinarySearchTree();
            LoadProductIndexes();
        }

        private void LoadProductIndexes()
        {
            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                _barcodeIndex.Add(product.Barcode, product);
                _nameIndex.Insert(product);
            }
        }

        public void AddProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Title))
                throw new ArgumentException("Product title is required.");

            if (string.IsNullOrWhiteSpace(product.Barcode))
                throw new ArgumentException("Barcode is required.");

            if (product.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (product.QuantityInStock < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            if (_context.Products.Any(p => p.Barcode == product.Barcode))
                throw new InvalidOperationException("A product with this barcode already exists.");

            _context.Products.Add(product);
            _context.SaveChanges();

            // Create a stock record for the new product.
            var stock = new Stock
            {
                ProductId = product.ProductId,
                QuantityAvailable = product.QuantityInStock,
                LowStockThreshold = 5,
                LastUpdated = DateTime.Now
            };
            _context.Stock.Add(stock);
            _context.SaveChanges();

            _barcodeIndex.Add(product.Barcode, product);
            _nameIndex.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (product.QuantityInStock < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            var existing = _context.Products.Find(product.ProductId);
            if (existing == null)
                throw new InvalidOperationException("Product not found.");

            existing.Title = product.Title;
            existing.Brand = product.Brand;
            existing.Price = product.Price;
            existing.QuantityInStock = product.QuantityInStock;
            existing.StockAvailabilityStatus = product.StockAvailabilityStatus;
            existing.ExpiryDate = product.ExpiryDate;
            existing.RestockDate = product.RestockDate;
            existing.SupplierId = product.SupplierId;
            existing.CategoryId = product.CategoryId;

            _context.Update(existing);
            _context.SaveChanges();

            _barcodeIndex.Add(existing.Barcode, existing);
            _nameIndex.Insert(existing);
        }

        public void RemoveProduct(string barcode)
        {
            var product = _context.Products.FirstOrDefault(p => p.Barcode == barcode);
            if (product == null)
                return;

            _context.Products.Remove(product);
            _context.SaveChanges();
            _barcodeIndex.Remove(barcode);
        }

        public Product SearchByBarcode(string barcode)
        {
            var product = _barcodeIndex.Search(barcode);
            if (product != null)
                return product;

            return _context.Products.FirstOrDefault(p => p.Barcode == barcode);
        }

       public Product? SearchProductByName(string title)
        {
            title = title.Trim().ToLower();

            return _context.Products
                .FirstOrDefault(p => p.Title.ToLower() == title);
        }

        public Product? SearchByName(string title)
        {
            return SearchProductByName(title);
        }
    }
}
