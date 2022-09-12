using AutoMapper;
using FluentAssertions;
using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Repositories.Interfaces;
using RetailShopWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Tests.RetailShopWebAPI.Services
{
    public class OrderServicesTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderServicesTests()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _productRepository = A.Fake<IProductRepository>();
            _customerRepository = A.Fake<ICustomerRepository>();
            _addressRepository = A.Fake<IAddressRepository>();
            _orderDetailRepository = A.Fake<IOrderDetailRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void CreateNewOrder_CustomerAddressAndProductExists_ReturnsTrue()
        {
            //Assign
            var QuantityAvailable = 10;
            var orderToCreate = A.Fake<CreateOrderDto>();
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;

            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderDetail.ProductId = 1;
            orderDetail.Quantity = 1;

            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);
            
            A.CallTo(() => _customerRepository.CustomerExists(orderToCreate.CustomerId)).Returns(true);
            A.CallTo(() => _addressRepository.AddressExists(orderToCreate.AddressId)).Returns(true);
            A.CallTo(() => _productRepository.ProductExists(orderDetail.ProductId)).Returns(true);
            A.CallTo(() => _productRepository.ProductAvailable(orderDetail.ProductId, orderDetail.Quantity, 0)).Returns(true);

            var customer = A.Fake<Customer>();
            A.CallTo(() => _customerRepository.GetCustomer(orderToCreate.CustomerId)).Returns(customer);
            var address = A.Fake<Address>();
            A.CallTo(() => _addressRepository.GetAddress(orderToCreate.AddressId)).Returns(address);
            var product = A.Fake<Product>();
            product.UnitsInStock = QuantityAvailable;
            A.CallTo(() => _productRepository.GetProduct(orderDetail.ProductId)).Returns(product);

            var order = A.Fake<Order>();
            var detail = A.Fake<OrderDetail>();
            order.OrderDetails = A.Fake<List<OrderDetail>>();
            order.OrderDetails.Add(detail);
            A.CallTo(() => _mapper.Map<Order>(orderToCreate)).Returns(order);


            A.CallTo(() => _orderRepository.AddNewOrder(order)).Returns(true);
            var service = new OrderServices(_orderRepository, _productRepository, 
                                            _customerRepository, _addressRepository, 
                                            _orderDetailRepository, _mapper);

            bool result = false;

            //Act
            Action act = () => result = service.CreateNewOrder(orderToCreate);
            
            //Assert
            act.Should().NotThrow<ApplicationException>();
            result.Should().Be(true);
            
        }

        [Theory]
        [InlineData(false, true, true)]
        [InlineData(false, true, false)]
        [InlineData(false, false, true)]
        [InlineData(false, false, false)]
        public void CreateNewOrder_CustomerNotFound_ThrowsApplicationException(bool IsCustomerFound, bool IsAddressFound, bool IsProductFound)
        {
            //Assign
            var QuantityAvailable = 10;
            var orderToCreate = A.Fake<CreateOrderDto>();
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;

            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderDetail.ProductId = 1;
            orderDetail.Quantity = 1;

            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);

            A.CallTo(() => _customerRepository.CustomerExists(orderToCreate.CustomerId)).Returns(IsCustomerFound);
            A.CallTo(() => _addressRepository.AddressExists(orderToCreate.AddressId)).Returns(IsAddressFound);
            A.CallTo(() => _productRepository.ProductExists(orderDetail.ProductId)).Returns(IsProductFound);
            A.CallTo(() => _productRepository.ProductAvailable(orderDetail.ProductId, orderDetail.Quantity, 0)).Returns(true);

            var customer = A.Fake<Customer>();
            A.CallTo(() => _customerRepository.GetCustomer(orderToCreate.CustomerId)).Returns(customer);
            var address = A.Fake<Address>();
            A.CallTo(() => _addressRepository.GetAddress(orderToCreate.AddressId)).Returns(address);
            var product = A.Fake<Product>();
            product.UnitsInStock = QuantityAvailable;
            A.CallTo(() => _productRepository.GetProduct(orderDetail.ProductId)).Returns(product);

            var order = A.Fake<Order>();
            var detail = A.Fake<OrderDetail>();
            order.OrderDetails = A.Fake<List<OrderDetail>>();
            order.OrderDetails.Add(detail);
            A.CallTo(() => _mapper.Map<Order>(orderToCreate)).Returns(order);


            A.CallTo(() => _orderRepository.AddNewOrder(order)).Returns(true);
            var service = new OrderServices(_orderRepository, _productRepository,
                                            _customerRepository, _addressRepository,
                                            _orderDetailRepository, _mapper);

            bool result = false;

            //Act
            Action act = () => result = service.CreateNewOrder(orderToCreate);

            //Assert
            act.Should().Throw<ApplicationException>().Where(e=>e.Message.Contains("Customer not found"));
        }

        [Theory]
        [InlineData(true, false, true)]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, false, false)]
        public void CreateNewOrder_AddressNotFound_ThrowsApplicationException(bool IsCustomerFound, bool IsAddressFound, bool IsProductFound)
        {
            //Assign
            var QuantityAvailable = 10;
            var orderToCreate = A.Fake<CreateOrderDto>();
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;

            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderDetail.ProductId = 1;
            orderDetail.Quantity = 1;

            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);

            A.CallTo(() => _customerRepository.CustomerExists(orderToCreate.CustomerId)).Returns(IsCustomerFound);
            A.CallTo(() => _addressRepository.AddressExists(orderToCreate.AddressId)).Returns(IsAddressFound);
            A.CallTo(() => _productRepository.ProductExists(orderDetail.ProductId)).Returns(IsProductFound);
            A.CallTo(() => _productRepository.ProductAvailable(orderDetail.ProductId, orderDetail.Quantity, 0)).Returns(true);

            var customer = A.Fake<Customer>();
            A.CallTo(() => _customerRepository.GetCustomer(orderToCreate.CustomerId)).Returns(customer);
            var address = A.Fake<Address>();
            A.CallTo(() => _addressRepository.GetAddress(orderToCreate.AddressId)).Returns(address);
            var product = A.Fake<Product>();
            product.UnitsInStock = QuantityAvailable;
            A.CallTo(() => _productRepository.GetProduct(orderDetail.ProductId)).Returns(product);

            var order = A.Fake<Order>();
            var detail = A.Fake<OrderDetail>();
            order.OrderDetails = A.Fake<List<OrderDetail>>();
            order.OrderDetails.Add(detail);
            A.CallTo(() => _mapper.Map<Order>(orderToCreate)).Returns(order);


            A.CallTo(() => _orderRepository.AddNewOrder(order)).Returns(true);
            var service = new OrderServices(_orderRepository, _productRepository,
                                            _customerRepository, _addressRepository,
                                            _orderDetailRepository, _mapper);

            bool result = false;

            //Act
            Action act = () => result = service.CreateNewOrder(orderToCreate);

            //Assert
            act.Should().Throw<ApplicationException>().Where(e => e.Message.Contains("Address not found"));
        }

        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void CreateNewOrder_ProductNotFound_ThrowsApplicationException(bool IsCustomerFound, bool IsAddressFound, bool IsProductFound)
        {
            //Assign
            var QuantityAvailable = 10;
            var orderToCreate = A.Fake<CreateOrderDto>();
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;

            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderDetail.ProductId = 1;
            orderDetail.Quantity = 1;

            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);

            A.CallTo(() => _customerRepository.CustomerExists(orderToCreate.CustomerId)).Returns(IsCustomerFound);
            A.CallTo(() => _addressRepository.AddressExists(orderToCreate.AddressId)).Returns(IsAddressFound);
            A.CallTo(() => _productRepository.ProductExists(orderDetail.ProductId)).Returns(IsProductFound);
            A.CallTo(() => _productRepository.ProductAvailable(orderDetail.ProductId, orderDetail.Quantity, 0)).Returns(true);

            var customer = A.Fake<Customer>();
            A.CallTo(() => _customerRepository.GetCustomer(orderToCreate.CustomerId)).Returns(customer);
            var address = A.Fake<Address>();
            A.CallTo(() => _addressRepository.GetAddress(orderToCreate.AddressId)).Returns(address);
            var product = A.Fake<Product>();
            product.UnitsInStock = QuantityAvailable;
            A.CallTo(() => _productRepository.GetProduct(orderDetail.ProductId)).Returns(product);

            var order = A.Fake<Order>();
            var detail = A.Fake<OrderDetail>();
            order.OrderDetails = A.Fake<List<OrderDetail>>();
            order.OrderDetails.Add(detail);
            A.CallTo(() => _mapper.Map<Order>(orderToCreate)).Returns(order);


            A.CallTo(() => _orderRepository.AddNewOrder(order)).Returns(true);
            var service = new OrderServices(_orderRepository, _productRepository,
                                            _customerRepository, _addressRepository,
                                            _orderDetailRepository, _mapper);

            bool result = false;

            //Act
            Action act = () => result = service.CreateNewOrder(orderToCreate);

            //Assert
            act.Should().Throw<ApplicationException>().Where(e => e.Message.Contains("Product not found"));
        }

        [Fact]
        public void CreateNewOrder_ProductUnavailable_ThrowsApplicationException()
        {
            //Assign
            var QuantityAvailable = 10;
            var orderToCreate = A.Fake<CreateOrderDto>();
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;

            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderDetail.ProductId = 1;
            orderDetail.Quantity = 1;

            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);

            A.CallTo(() => _customerRepository.CustomerExists(orderToCreate.CustomerId)).Returns(true);
            A.CallTo(() => _addressRepository.AddressExists(orderToCreate.AddressId)).Returns(true);
            A.CallTo(() => _productRepository.ProductExists(orderDetail.ProductId)).Returns(true);
            A.CallTo(() => _productRepository.ProductAvailable(orderDetail.ProductId, orderDetail.Quantity, 0)).Returns(false);

            var customer = A.Fake<Customer>();
            A.CallTo(() => _customerRepository.GetCustomer(orderToCreate.CustomerId)).Returns(customer);
            var address = A.Fake<Address>();
            A.CallTo(() => _addressRepository.GetAddress(orderToCreate.AddressId)).Returns(address);
            var product = A.Fake<Product>();
            product.UnitsInStock = QuantityAvailable;
            A.CallTo(() => _productRepository.GetProduct(orderDetail.ProductId)).Returns(product);

            var order = A.Fake<Order>();
            var detail = A.Fake<OrderDetail>();
            order.OrderDetails = A.Fake<List<OrderDetail>>();
            order.OrderDetails.Add(detail);
            A.CallTo(() => _mapper.Map<Order>(orderToCreate)).Returns(order);


            A.CallTo(() => _orderRepository.AddNewOrder(order)).Returns(true);
            var service = new OrderServices(_orderRepository, _productRepository,
                                            _customerRepository, _addressRepository,
                                            _orderDetailRepository, _mapper);

            bool result = false;

            //Act
            Action act = () => result = service.CreateNewOrder(orderToCreate);

            //Assert
            act.Should().Throw<ApplicationException>().Where(e => e.Message.Contains("Product unavailable"));
        }

        [Fact]
        public void DeleteOrder_OrderNotFoundToDelete_ThrowsKeyNotFoundException()
        {
            //Assign
            var orderId = 900;
            A.CallTo(()=>_orderRepository.OrderExists(orderId)).Returns(false);

            var orderToDelete = A.Fake<Order>();
            A.CallTo(() => _orderRepository.GetOrder(orderId)).Returns(orderToDelete);
            A.CallTo(() => _orderRepository.DeleteOrder(orderToDelete)).Returns(true);

            var service = new OrderServices(_orderRepository, _productRepository,
                                          _customerRepository, _addressRepository,
                                          _orderDetailRepository, _mapper);

            //Act
            Action act = () => service.DeleteOrder(orderId);

            //Assert
            act.Should().Throw<KeyNotFoundException>().Where(e => e.Message.Contains("Order not found"));
        }
    }
}
