using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WebStore.Models.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Products product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity = quantity;
            }
        }

        public void RemoveLine(Products product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputyTotalValue()
        {
            
            return lineCollection.Sum(c => c.Product.Price * c.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

    }
    public class CartLine
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}
