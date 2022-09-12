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
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _dataContext;

        public AddressRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AddNewAddress(Address address)
        {
            _dataContext.Add(address);
            _dataContext.ChangeTracker.Entries<Customer>().ToList().ForEach(p => p.State = EntityState.Unchanged);
            return Save();
        }

        public bool AddressExists(int addressId)
        {
            return _dataContext.Addresses.AsNoTracking().Any(a => a.AddressId == addressId);
        }

        public bool DeleteAddress(Address address)
        {
            _dataContext.Remove(address);
            return Save();
        }

        public Address? GetAddress(int addressId)
        {
            return _dataContext.Addresses.AsNoTracking().Where(a => a.AddressId == addressId).FirstOrDefault();
        }

        public List<Address> GetAllAddressesForCustomer(int customerId)
        {
            return _dataContext.Addresses.AsNoTracking()
                .Where(a => a.Customer.CustomerId == customerId)
                .OrderBy(a => a.AddressId).ToList();
        }

        public bool UpdateAddress(Address address)
        {
            _dataContext.Update(address);
            return Save();
        }

        public bool Save()
        {
            return (_dataContext.SaveChanges() > 0);
        }

    }
}
