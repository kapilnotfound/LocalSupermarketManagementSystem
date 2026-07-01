using System;
using System.Collections.Generic;
using System.Linq;
using SupermarketApp.Data;
using SupermarketApp.Models;

namespace SupermarketApp.Services
{
    public class SupplierService
    {
        private readonly SupermarketDbContext _context;

        public SupplierService(SupermarketDbContext context)
        {
            _context = context;
        }

        public void AddSupplier(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                throw new ArgumentException("Supplier name is required.");

            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            var existing = _context.Suppliers.Find(supplier.SupplierId);
            if (existing == null)
                throw new InvalidOperationException("Supplier not found.");

            existing.SupplierName = supplier.SupplierName;
            existing.ContactPerson = supplier.ContactPerson;
            existing.Phone = supplier.Phone;
            existing.Email = supplier.Email;
            existing.Address = supplier.Address;

            _context.Update(existing);
            _context.SaveChanges();
        }

        public void RemoveSupplier(int supplierId)
        {
            var existing = _context.Suppliers.Find(supplierId);
            if (existing == null)
                return;

            _context.Suppliers.Remove(existing);
            _context.SaveChanges();
        }

        public List<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }
    }
}
