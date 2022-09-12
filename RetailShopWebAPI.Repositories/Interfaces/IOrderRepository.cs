using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrdersForACustomer(int customerId);
        Order? GetOrder(int orderId);
        bool OrderExists(int orderId);
        bool AddNewOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
