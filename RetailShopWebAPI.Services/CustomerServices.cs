using AutoMapper;
using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Repositories.Interfaces;
using RetailShopWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public CustomerServices(ICustomerRepository customerRepository,IAddressRepository addressRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public bool CreateCustomer(CreateCustomerDto customer)
        {
            var customers = _customerRepository.GetAllCustomers();
            if (customers.Any(c => c.EmailId.ToUpper().Trim() == customer.EmailId.ToUpper().Trim()))
            {
                throw new ApplicationException("Email Id is already used by another customer!");
            }

            return _customerRepository.AddNewCustomer(_mapper.Map<Customer>(customer));
        }

        public bool DeleteCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                throw new KeyNotFoundException("Customer not found!");
            }
            var customerToDelete = _customerRepository.GetCustomer(customerId);
            return _customerRepository.DeleteCustomer(customerToDelete);
        }

        public List<CustomerDto> GetAllCustomers()
        {
            return _mapper.Map<List<CustomerDto>>(_customerRepository.GetAllCustomers());
        }

        public CustomerDto? GetCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                throw new KeyNotFoundException("Customer not found!");
            }
            return _mapper.Map<CustomerDto>(_customerRepository.GetCustomer(customerId));
        }

        public bool UpdateCustomer(CustomerDto customer)
        {
            if (!_customerRepository.CustomerExists(customer.CustomerId))
            {
                throw new KeyNotFoundException("Customer not found!");
            }
            var customers = _customerRepository.GetAllCustomers();
            if (customers.Any(c => c.EmailId.ToUpper().Trim() == customer.EmailId.ToUpper().Trim() && c.CustomerId != customer.CustomerId))
            {
                throw new ApplicationException("Email Id is already used by another customer!");
            }

            var customerToUpdate = _mapper.Map<Customer>(customer);
            List<string> ErrorMessage = new List<string>();
            foreach (var address in customerToUpdate.Addresses)
            {
                if (address.AddressId != 0 && !_addressRepository.AddressExists(address.AddressId))
                    ErrorMessage.Add("Address not found! AddressId: " + address.AddressId);
            }

            if (ErrorMessage.Count > 0)
                throw new ApplicationException(String.Join(", ", ErrorMessage));

            return _customerRepository.UpdateCustomer(customerToUpdate);
        }
    }
}
