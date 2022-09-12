using RetailShopWebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Services.Interfaces
{
    public interface IOrderServices
    {
        List<OrderDto> GetAllOrdersForACustomer(int customerId);
        OrderDto? GetOrder(int orderId);
        bool CreateNewOrder(CreateOrderDto order);
        bool UpdateOrder(UpdateOrderDto order);
        bool DeleteOrder(int orderId);

    }
}
