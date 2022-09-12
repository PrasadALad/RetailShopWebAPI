using RetailShopWebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Services.Interfaces
{
    public interface ICustomerServices
    {
        List<CustomerDto> GetAllCustomers();
        CustomerDto? GetCustomer(int customerId);
        bool CreateCustomer(CreateCustomerDto customer);
        bool UpdateCustomer(CustomerDto customer);
        bool DeleteCustomer(int customerId);
    }
}
