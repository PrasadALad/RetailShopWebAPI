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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;

        public CustomerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool AddNewCustomer(Customer customer)
        {
            _dataContext.Add(customer);
            return Save();
        }

        public bool CustomerExists(int customerId)
        {
            return _dataContext.Customers.AsNoTracking().Any(c => c.CustomerId == customerId);
        }

        public bool DeleteCustomer(Customer customer)
        {
            _dataContext.Remove(customer);
            return Save();
        }

        public List<Customer> GetAllCustomers()
        {
            return _dataContext.Customers.AsNoTracking().Include(c => c.Addresses).OrderBy(c => c.CustomerId).ToList();
        }

        public Customer? GetCustomer(int customerId)
        {
            return _dataContext.Customers.AsNoTracking().Where(c => c.CustomerId == customerId).Include(c => c.Addresses).FirstOrDefault();
        }

        public bool UpdateCustomer(Customer customer)
        {
            _dataContext.Update(customer);
            return Save();
        }

        public bool Save()
        {
            return (_dataContext.SaveChanges() > 0);
        }

    }
}
