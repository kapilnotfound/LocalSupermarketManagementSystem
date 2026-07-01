using System;
using System.Linq;
using SupermarketApp.Data;

namespace SupermarketApp.Services
{
    public class ReportService
    {
        private readonly SupermarketDbContext _context;

        public ReportService(SupermarketDbContext context)
        {
            _context = context;
        }

        public void PrintLowStockReport()
        {
            Console.WriteLine("-- Low Stock Report --");
            var lowStockItems = _context.Stock
                .Where(s => s.QuantityAvailable <= s.LowStockThreshold)
                .Select(s => new
                {
                    s.Product.Title,
                    s.Product.Barcode,
                    s.QuantityAvailable,
                    s.LowStockThreshold
                })
                .ToList();

            if (!lowStockItems.Any())
            {
                Console.WriteLine("No low stock items found.");
                return;
            }

            foreach (var item in lowStockItems)
            {
                Console.WriteLine($"{item.Title} ({item.Barcode}) - Qty: {item.QuantityAvailable}, Threshold: {item.LowStockThreshold}");
            }
        }

        public void PrintSalesByProduct()
        {
            Console.WriteLine("-- Sales by Product Report --");
            var salesByProduct = _context.SaleItems
                .GroupBy(si => si.Product.Title)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalQuantity = g.Sum(si => si.QuantitySold),
                    TotalSales = g.Sum(si => si.LineTotal)
                })
                .ToList();

            if (!salesByProduct.Any())
            {
                Console.WriteLine("No sales data available.");
                return;
            }

            foreach (var row in salesByProduct)
            {
                Console.WriteLine($"{row.ProductName} - Quantity sold: {row.TotalQuantity}, Sales: {row.TotalSales:C}");
            }
        }

        public void PrintProductsByCategory()
        {
            Console.WriteLine("-- Products by Category Report --");
            var productsByCategory = _context.Products
                .GroupBy(p => p.Category.CategoryName)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToList();

            if (!productsByCategory.Any())
            {
                Console.WriteLine("No products or categories available.");
                return;
            }

            foreach (var row in productsByCategory)
            {
                Console.WriteLine($"{row.Category} - {row.Count} products");
            }
        }

        public void PrintSupplierStockList()
        {
            Console.WriteLine("-- Supplier Stock List --");
            var supplierStock = _context.Products
                .GroupBy(p => p.Supplier.SupplierName)
                .Select(g => new
                {
                    Supplier = g.Key,
                    TotalProducts = g.Count(),
                    TotalStock = g.Sum(p => p.QuantityInStock)
                })
                .ToList();

            if (!supplierStock.Any())
            {
                Console.WriteLine("No supplier stock data available.");
                return;
            }

            foreach (var row in supplierStock)
            {
                Console.WriteLine($"{row.Supplier} - Products: {row.TotalProducts}, Total stock: {row.TotalStock}");
            }
        }
    }
}
