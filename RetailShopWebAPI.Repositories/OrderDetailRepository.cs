using Microsoft.EntityFrameworkCore;
using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Repositories.Data;
using RetailShopWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DataContext _dataContext;

        public OrderDetailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateNewOrderDetail(OrderDetail orderDetail)
        {
            _dataContext.Add(orderDetail);
            _dataContext.ChangeTracker.Entries<Order>().ToList().ForEach(p => p.State = EntityState.Unchanged);
            _dataContext.ChangeTracker.Entries<Product>().ToList().ForEach(p => p.State = EntityState.Unchanged);

            return Save();
        }

        public bool DeleteOrderDetail(OrderDetail orderDetail)
        {
            List<int> orderIds = _dataContext.OrderDetails.AsNoTracking()
                .Where(od => od.OrderDetailId == orderDetail.OrderDetailId)
                .Select(od => od.Order.OrderId).ToList();

            List<Order> orders = _dataContext.Orders.AsNoTracking().Where(od => orderIds.Contains(od.OrderId))
                .Include(od => od.OrderDetails).ToList();
            orders = (from i in orders where i.OrderDetails.Count <= 1 select i).ToList();
            foreach(var order in orders)
            {
                order.Address = null;
                order.OrderDetails = null;
                order.Customer = null;
            }

            _dataContext.Remove(orderDetail);
            if (orders != null && orders.Count > 0)
                _dataContext.RemoveRange(orders);

            return Save();
        }

        public OrderDetail? GetOrderDetail(int orderDetailId)
        {
            return _dataContext.OrderDetails.AsNoTracking().Where(od => od.OrderDetailId == orderDetailId).FirstOrDefault();
        }

        public int GetOrderDetailQuantity(int orderDetailId)
        {
            return _dataContext.OrderDetails.AsNoTracking().Where(od => od.OrderDetailId == orderDetailId)
                .Select(od => od.Quantity).FirstOrDefault();
        }

        public bool OrderDetailExists(int orderDetailId)
        {
            return _dataContext.OrderDetails.AsNoTracking().Any(od => od.OrderDetailId == orderDetailId);
        }

        public bool Save()
        {
            return (_dataContext.SaveChanges() > 0);
        }
    }
}
