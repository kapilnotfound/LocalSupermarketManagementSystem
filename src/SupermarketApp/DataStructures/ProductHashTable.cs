using System;
using System.Collections.Generic;
using SupermarketApp.Models;

namespace SupermarketApp.DataStructures
{
    public class ProductHashTable
    {
        private class Node
        {
            public string Key { get; set; }
            public Product Product { get; set; }
            public Node Next { get; set; }

            public Node(string key, Product product)
            {
                Key = key;
                Product = product;
            }
        }

        private readonly Node[] buckets;

        public ProductHashTable(int size = 23)
        {
            // Use a small prime number for a beginner-friendly hash table.
            buckets = new Node[size];
        }

        private int GetBucketIndex(string key)
        {
            // Simple hash code from barcode string.
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }

        public void Add(string barcode, Product product)
        {
            var index = GetBucketIndex(barcode);
            var node = buckets[index];

            if (node == null)
            {
                buckets[index] = new Node(barcode, product);
                return;
            }

            // Collision handling with linked nodes.
            Node previous = null;
            while (node != null)
            {
                if (node.Key == barcode)
                {
                    node.Product = product;
                    return;
                }
                previous = node;
                node = node.Next;
            }
            previous.Next = new Node(barcode, product);
        }

        public Product Search(string barcode)
        {
            var index = GetBucketIndex(barcode);
            var node = buckets[index];
            while (node != null)
            {
                if (node.Key == barcode)
                    return node.Product;
                node = node.Next;
            }
            return null;
        }

        public bool Remove(string barcode)
        {
            var index = GetBucketIndex(barcode);
            var node = buckets[index];
            Node previous = null;

            while (node != null)
            {
                if (node.Key == barcode)
                {
                    if (previous == null)
                        buckets[index] = node.Next;
                    else
                        previous.Next = node.Next;
                    return true;
                }
                previous = node;
                node = node.Next;
            }
            return false;
        }

        public IEnumerable<Product> DisplayAll()
        {
            for (var i = 0; i < buckets.Length; i++)
            {
                var node = buckets[i];
                while (node != null)
                {
                    yield return node.Product;
                    node = node.Next;
                }
            }
        }
    }
}
