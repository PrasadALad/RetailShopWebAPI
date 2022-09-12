using RetailShopWebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Services.Interfaces
{
    public interface IOrderDetailServices
    {
        bool DeleteOrderDetail(int orderDetailId);
        bool CreateNewOrderDetail(CreateOrderDetailDto createOrderDetailDto);
    }
}
