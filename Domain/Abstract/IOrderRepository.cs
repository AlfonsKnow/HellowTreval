using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IOrderRepository
    {
        void SaveOrder(Order format);
        void RemoveOrder(Order format);
        IEnumerable<Order> GetAllOrders();
    }
}
