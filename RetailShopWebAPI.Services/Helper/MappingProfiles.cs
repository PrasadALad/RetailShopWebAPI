using AutoMapper;
using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Common.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<CreateCustomerAddressDto, Address>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailDto>();

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderOrderDetailDto, OrderDetail>();

            CreateMap<UpdateOrderDto, Order>();
            CreateMap<UpdateOrderDetailDto, OrderDetail>();

            CreateMap<CreateOrderDetailDto, OrderDetail>();
        }
    }
}
