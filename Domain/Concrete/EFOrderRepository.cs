using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders.Include("User").ToList();
        }

        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders.Find(order.OrderId);
                if (dbEntry != null)
                {
                    dbEntry.Sum = order.Sum;
                    dbEntry.UserId = order.UserId;
                }
            }
            context.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            context.Orders.Attach(order);
            context.Orders.Remove(order);

            context.SaveChanges();
        }
    }
}
