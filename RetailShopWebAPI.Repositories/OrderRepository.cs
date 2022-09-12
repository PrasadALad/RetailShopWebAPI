using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool AddNewOrder(Order order)
        {
            _dataContext.Add(order);
            _dataContext.ChangeTracker.Entries<Customer>().ToList().ForEach(p => p.State = EntityState.Unchanged);
            _dataContext.ChangeTracker.Entries<Product>().ToList().ForEach(p => p.State = EntityState.Unchanged);
            _dataContext.ChangeTracker.Entries<Address>().ToList().ForEach(p => p.State = EntityState.Unchanged);
            _dataContext.UpdateRange((from od in order.OrderDetails select od.Product).ToList());
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _dataContext.Remove(order);
            return Save();
        }

        public List<Order> GetAllOrdersForACustomer(int customerId)
        {
            return _dataContext.Orders.AsNoTracking().Where(o => o.Customer.CustomerId == customerId)
                .Include(o => o.Address)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product).ToList();
        }

        public Order? GetOrder(int orderId)
        {
            return _dataContext.Orders.AsNoTracking().Where(o => o.OrderId == orderId)
                .Include(o => o.Address)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .FirstOrDefault();
        }

        public bool OrderExists(int orderId)
        {
            return _dataContext.Orders.AsNoTracking().Any(o => o.OrderId == orderId);
        }

        public bool UpdateOrder(Order order)
        {
            _dataContext.Update(order);
            if(order.OrderDetails != null && order.OrderDetails.Count != 0)
            {
                _dataContext.UpdateRange((from od in order.OrderDetails select od.Product).ToList());
            }
            return Save();
        }
        public bool Save()
        {
            return (_dataContext.SaveChanges() > 0);
        }

    }
}
