using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        OrderDetail? GetOrderDetail(int orderDetailId);
        bool OrderDetailExists(int orderDetailId);
        bool DeleteOrderDetail(OrderDetail orderDetail);
        bool CreateNewOrderDetail(OrderDetail orderDetail);
        int GetOrderDetailQuantity(int orderDetailId);
        bool Save();

    }
}
