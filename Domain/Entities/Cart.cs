using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Tour tour, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Tour.TourId == tour.TourId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Tour = tour, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Tour tour)
        {
            lineCollection.RemoveAll(l => l.Tour.TourId == tour.TourId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Tour.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Tour Tour { get; set; }
        public int Quantity { get; set; }
    }
}
