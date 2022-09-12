using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Services.Interfaces
{
    public interface IAddressServices
    {
        List<AddressDto> GetAllAddressesForCustomer(int customerId);
        AddressDto? GetAddress(int addressId);
        bool AddNewAddress(CreateAddressDto address);
        bool UpdateAddress(AddressDto address);
        bool DeleteAddress(int addressId);
    }
}
