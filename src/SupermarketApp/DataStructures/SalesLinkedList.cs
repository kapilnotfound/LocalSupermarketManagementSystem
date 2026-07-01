using System;
using System.Collections.Generic;
using SupermarketApp.Models;

namespace SupermarketApp.DataStructures
{
    public class SalesLinkedList
    {
        private class Node
        {
            public Sale Sale { get; set; }
            public Node Next { get; set; }

            public Node(Sale sale)
            {
                Sale = sale;
            }
        }

        private Node head;
        private int count;

        public void AddSale(Sale sale)
        {
            var node = new Node(sale);
            if (head == null)
            {
                head = node;
            }
            else
            {
                var current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node;
            }
            count++;
        }

        public IEnumerable<Sale> DisplaySales()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Sale;
                current = current.Next;
            }
        }

        public int CountSales()
        {
            return count;
        }
    }
}
