using System;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data;
using SupermarketApp.DataStructures;
using SupermarketApp.Models;
using Xunit;

namespace SupermarketTests
{
    public class SearchTests
    {
        [Fact]
        public void SearchByBarcode_ReturnsProduct()
        {
            var hashTable = new ProductHashTable();
            var product = new Product { Title = "Cereal", Barcode = "888999000", Price = 3.00m, QuantityInStock = 10 };
            hashTable.Add(product.Barcode, product);

            var result = hashTable.Search("888999000");
            Assert.NotNull(result);
            Assert.Equal("Cereal", result.Title);
        }

        [Fact]
        public void SearchByName_ReturnsProduct()
        {
            var bst = new ProductBinarySearchTree();
            var product = new Product { Title = "Shampoo", Barcode = "555444333", Price = 4.00m, QuantityInStock = 7 };
            bst.Insert(product);

            var result = bst.SearchByName("Shampoo");
            Assert.NotNull(result);
            Assert.Equal("555444333", result.Barcode);
        }

        [Fact]
        public void Search_ReturnsNullIfNotFound()
        {
            var hashTable = new ProductHashTable();
            var bst = new ProductBinarySearchTree();
            Assert.Null(hashTable.Search("missing"));
            Assert.Null(bst.SearchByName("missing"));
        }
    }
}
