using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        List<Address> GetAllAddressesForCustomer(int customerId);
        Address? GetAddress(int addressId);
        bool AddressExists(int addressId);
        bool AddNewAddress(Address address);
        bool UpdateAddress(Address address);
        bool DeleteAddress(Address address);
        bool Save();
    }
}
