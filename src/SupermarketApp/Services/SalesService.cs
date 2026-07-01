using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupermarketApp.Data;
using SupermarketApp.Models;

namespace SupermarketApp.Services
{
    public class SalesService
    {
        private readonly SupermarketDbContext _context;
        private readonly StockService _stockService;

        public SalesService(SupermarketDbContext context, StockService stockService)
        {
            _context = context;
            _stockService = stockService;
        }

        public SaleResult RecordSale(List<(string barcode, int quantity)> items)
        {
            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                TotalAmount = 0m
            };

            var receipt = new StringBuilder();
            receipt.AppendLine("--- Sale Receipt ---");

            foreach (var item in items)
            {
                if (item.quantity <= 0)
                {
                    return new SaleResult(false, "Quantity must be greater than zero.");
                }

                var product = _context.Products.FirstOrDefault(p => p.Barcode == item.barcode);
                if (product == null)
                    return new SaleResult(false, $"Product not found: {item.barcode}");

                var stock = _context.Stock.FirstOrDefault(s => s.ProductId == product.ProductId);
                var available = stock?.QuantityAvailable ?? product.QuantityInStock;
                if (available < item.quantity)
                {
                    return new SaleResult(false, $"Not enough stock for {product.Title}.");
                }

                var lineTotal = item.quantity * product.Price;
                sale.SaleItems.Add(new SaleItem
                {
                    ProductId = product.ProductId,
                    QuantitySold = item.quantity,
                    UnitPrice = product.Price,
                    LineTotal = lineTotal
                });

                sale.TotalAmount += lineTotal;
                receipt.AppendLine($"{product.Title} x{item.quantity} @ {product.Price:C} = {lineTotal:C}");

                var newQuantity = available - item.quantity;
                if (stock != null)
                {
                    stock.QuantityAvailable = newQuantity;
                    stock.LastUpdated = DateTime.Now;
                    _context.Stock.Update(stock);
                }

                product.QuantityInStock = newQuantity;
                product.StockAvailabilityStatus = newQuantity <= 0 ? "Out of stock" : "In stock";
                _context.Products.Update(product);
            }

            _context.Sales.Add(sale);
            _context.SaveChanges();

            receipt.AppendLine($"Total: {sale.TotalAmount:C}");
            receipt.AppendLine("Sale successfully recorded.");
            return new SaleResult(true, "Sale recorded", receipt.ToString());
        }
    }

    public class SaleResult
    {
        public bool Success { get; }
        public string Message { get; }
        public string Receipt { get; }

        public SaleResult(bool success, string message, string receipt = "")
        {
            Success = success;
            Message = message;
            Receipt = receipt;
        }
    }
}
