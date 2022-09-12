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
    public class AddressServices : IAddressServices
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public AddressServices(IAddressRepository addressRepository,ICustomerRepository customerRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public bool AddNewAddress(CreateAddressDto address)
        {
            if (!_customerRepository.CustomerExists(address.customerId))
            {
                throw new KeyNotFoundException("Customer not found for the address!");
            }
            var customer = _customerRepository.GetCustomer(address.customerId);
            
            var addressToAdd = _mapper.Map<Address>(address);
            customer.Addresses = null;
            addressToAdd.Customer = customer;
            return _addressRepository.AddNewAddress(addressToAdd);
        }

        public bool DeleteAddress(int addressId)
        {
            if (!_addressRepository.AddressExists(addressId))
            {
                throw new KeyNotFoundException("Address not found!");
            }
            var addressToDelete = _addressRepository.GetAddress(addressId);
            return _addressRepository.DeleteAddress(addressToDelete);
        }

        public AddressDto? GetAddress(int addressId)
        {
            if (!_addressRepository.AddressExists(addressId))
            {
                throw new KeyNotFoundException("Address not found!");
            }
            return _mapper.Map<AddressDto>(_addressRepository.GetAddress(addressId));
        }

        public List<AddressDto> GetAllAddressesForCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                throw new KeyNotFoundException("Customer not found!");
            }
            return _mapper.Map<List<AddressDto>>(_addressRepository.GetAllAddressesForCustomer(customerId));
        }

        public bool UpdateAddress(AddressDto address)
        {
            if (!_addressRepository.AddressExists(address.AddressId))
            {
                throw new KeyNotFoundException("Address not found!");
            }
            return _addressRepository.UpdateAddress(_mapper.Map<Address>(address));
        }
    }
}
