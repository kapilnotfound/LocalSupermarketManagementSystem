using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupermarketApp.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required]
        public string SupplierName { get; set; }

        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
