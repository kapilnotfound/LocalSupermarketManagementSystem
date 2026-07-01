using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupermarketApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Barcode { get; set; }

        public string Brand { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime ExpiryDate { get; set; }
        public DateTime RestockDate { get; set; }
        public string StockAvailabilityStatus { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public Stock Stock { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
