using System;
using System.Linq;
using SupermarketApp.Data;
using SupermarketApp.Models;

namespace SupermarketApp.Services
{
    public class StockService
    {
        private readonly SupermarketDbContext _context;

        public StockService(SupermarketDbContext context)
        {
            _context = context;
        }

        public void UpdateStock(int productId, int quantityAvailable)
        {
            if (quantityAvailable < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            var stock = _context.Stock.FirstOrDefault(s => s.ProductId == productId);
            var product = _context.Products.Find(productId);
            if (product == null)
                throw new InvalidOperationException("Product not found.");

            if (stock == null)
            {
                stock = new Stock
                {
                    ProductId = productId,
                    QuantityAvailable = quantityAvailable,
                    LowStockThreshold = 5,
                    LastUpdated = DateTime.Now
                };
                _context.Stock.Add(stock);
            }
            else
            {
                stock.QuantityAvailable = quantityAvailable;
                stock.LastUpdated = DateTime.Now;
                _context.Stock.Update(stock);
            }

            product.QuantityInStock = quantityAvailable;
            product.StockAvailabilityStatus = quantityAvailable <= 0 ? "Out of stock" : "In stock";
            _context.Products.Update(product);
        }

        public bool CheckLowStock(int productId)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.ProductId == productId);
            return stock != null && stock.QuantityAvailable <= stock.LowStockThreshold;
        }
    }
}
