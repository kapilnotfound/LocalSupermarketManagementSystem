using System;
using System.Collections.Generic;
using SupermarketApp.Models;

namespace SupermarketApp.DataStructures
{
    public class ProductBinarySearchTree
    {
        private class Node
        {
            public Product Product { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(Product product)
            {
                Product = product;
            }
        }

        private Node root;

        public void Insert(Product product)
        {
            root = InsertInternal(root, product);
        }

        private Node InsertInternal(Node node, Product product)
        {
            if (node == null)
            {
                return new Node(product);
            }

            var comparison = string.Compare(product.Title, node.Product.Title, StringComparison.OrdinalIgnoreCase);
            if (comparison < 0)
            {
                node.Left = InsertInternal(node.Left, product);
            }
            else if (comparison > 0)
            {
                node.Right = InsertInternal(node.Right, product);
            }
            else
            {
                node.Product = product;
            }

            return node;
        }

        public Product SearchByName(string title)
        {
            return SearchInternal(root, title);
        }

        private Product SearchInternal(Node node, string title)
        {
            if (node == null)
                return null;

            var comparison = string.Compare(title, node.Product.Title, StringComparison.OrdinalIgnoreCase);
            if (comparison == 0)
                return node.Product;

            return comparison < 0 ? SearchInternal(node.Left, title) : SearchInternal(node.Right, title);
        }

        public IEnumerable<Product> InOrderDisplay()
        {
            return InOrderInternal(root);
        }

        private IEnumerable<Product> InOrderInternal(Node node)
        {
            if (node == null)
                yield break;

            foreach (var item in InOrderInternal(node.Left))
                yield return item;

            yield return node.Product;

            foreach (var item in InOrderInternal(node.Right))
                yield return item;
        }
    }
}
