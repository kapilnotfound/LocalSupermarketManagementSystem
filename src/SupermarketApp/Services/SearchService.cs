using System.Collections.Generic;
using System.Linq;
using SupermarketApp.Data;
using SupermarketApp.Models;

namespace SupermarketApp.Services
{
    public class SearchService
    {
        private readonly SupermarketDbContext _context;

        public SearchService(SupermarketDbContext context)
        {
            _context = context;
        }

        public Product SearchByBarcode(string barcode)
        {
            return _context.Products.FirstOrDefault(p => p.Barcode == barcode);
        }

        public Product SearchByName(string title)
        {
            return _context.Products.FirstOrDefault(p => p.Title.ToLower() == title.ToLower());
        }

        public List<Product> SearchByCategory(string categoryName)
        {
            return _context.Products
                .Where(p => p.Category != null && p.Category.CategoryName.ToLower() == categoryName.ToLower())
                .ToList();
        }

        public List<Product> SearchBySupplier(string supplierName)
        {
            return _context.Products
                .Where(p => p.Supplier != null && p.Supplier.SupplierName.ToLower() == supplierName.ToLower())
                .ToList();
        }
    }
}
