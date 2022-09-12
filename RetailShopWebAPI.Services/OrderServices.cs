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
    public class OrderServices :IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderServices(IOrderRepository orderRepository,IProductRepository productRepository, 
            ICustomerRepository customerRepository,IAddressRepository addressRepository,
            IOrderDetailRepository orderDetailRepository
            ,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public bool CreateNewOrder(CreateOrderDto order)
        {
            List<string> ErrorMsgs = new List<string>();
            if (!_customerRepository.CustomerExists(order.CustomerId))
                ErrorMsgs.Add("Customer not found! CustomerId: " + order.CustomerId);

            if (!_addressRepository.AddressExists(order.AddressId))
                ErrorMsgs.Add("Address not found! AddressId: " + order.AddressId);

            foreach(var detail in order.OrderDetails)
            {
                if (!_productRepository.ProductExists(detail.ProductId))
                    ErrorMsgs.Add("Product not found! ProductId: " + detail.ProductId);
                else if (!_productRepository.ProductAvailable(detail.ProductId, detail.Quantity))
                    ErrorMsgs.Add("Product unavailable! ProductId:" + detail.ProductId);
            }
            if(ErrorMsgs.Count > 0)
            {
                throw new ApplicationException(String.Join(", ",ErrorMsgs));
            }

            var OrderToCreate = _mapper.Map<Order>(order);
            OrderToCreate.Customer = _customerRepository.GetCustomer(order.CustomerId);
            OrderToCreate.Customer.Addresses = null;
            OrderToCreate.Customer.Orders = null;

            OrderToCreate.Address = _addressRepository.GetAddress(order.AddressId);
            OrderToCreate.Address.Orders = null;
            OrderToCreate.Address.Customer = null;

            for(var i =0; i< OrderToCreate.OrderDetails.Count; i++)
            {
                var detailToCreate = OrderToCreate.OrderDetails[i];
                var detailReceived = order.OrderDetails[i];
                detailToCreate.Product = _productRepository.GetProduct(detailReceived.ProductId);
                detailToCreate.Product.UnitsInStock = detailToCreate.Product.UnitsInStock - detailReceived.Quantity;
                detailToCreate.Product.OrderDetails = null;
            }
           
            return _orderRepository.AddNewOrder(OrderToCreate);
        }

        public bool DeleteOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                throw new KeyNotFoundException("Order not found!");

            var orderToDelete = _orderRepository.GetOrder(orderId);
            return _orderRepository.DeleteOrder(orderToDelete);
        }

        public List<OrderDto> GetAllOrdersForACustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
                throw new KeyNotFoundException("Customer not found!");

            return _mapper.Map<List<OrderDto>>(_orderRepository.GetAllOrdersForACustomer(customerId));
        }

        public OrderDto? GetOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                throw new KeyNotFoundException("Order not found!");
            return _mapper.Map<OrderDto>(_orderRepository.GetOrder(orderId));
        }

        public bool UpdateOrder(UpdateOrderDto order)
        {
            List<Product> productsToUpdate = new List<Product>();
            List<string> ErrorMsgs = new List<string>();
            if (!_orderRepository.OrderExists(order.OrderId))
                ErrorMsgs.Add("Order not found!");

            if (order.AddressId != 0
                && !_addressRepository.AddressExists(order.AddressId))
                ErrorMsgs.Add("Address not found! AddressId: " + order.AddressId);

            if (order.OrderDetails != null && order.OrderDetails.Count > 0)
            {
                foreach(var details in order.OrderDetails)
                {
                    int currentQuantity = 0;
                    if(details.OrderDetailId != 0
                        && !_orderDetailRepository.OrderDetailExists(details.OrderDetailId))
                        ErrorMsgs.Add("Order detail not found! OrderDetailId: " + details.OrderDetailId);
                    else if(details.OrderDetailId != 0)
                    {
                        currentQuantity = _orderDetailRepository.GetOrderDetailQuantity(details.OrderDetailId);
                    }

                    if (details.ProductId == 0 ||
                        !_productRepository.ProductExists(details.ProductId))
                        ErrorMsgs.Add("Product not found! ProductId: " + details.ProductId);
                    else if (!_productRepository.ProductAvailable(details.ProductId, details.Quantity, currentQuantity))
                        ErrorMsgs.Add("Product unavailable! ProductId:" + details.ProductId);
                }
            }

            if (ErrorMsgs.Count > 0)
                throw new ApplicationException(String.Join(", ", ErrorMsgs));


            var OrderToUpdate = _mapper.Map<Order>(order);

            if(order.AddressId != 0)
            {
                OrderToUpdate.Address = _addressRepository.GetAddress(order.AddressId);
                OrderToUpdate.Address.Orders = null;
                OrderToUpdate.Address.Customer = null;
            }

            if(OrderToUpdate.OrderDetails != null && OrderToUpdate.OrderDetails.Count > 0)
            {
                for (var i = 0; i < OrderToUpdate.OrderDetails.Count; i++)
                {
                    var oldOrderQuantity = 0;
                    var detailToUpdate = OrderToUpdate.OrderDetails[i];
                    if(detailToUpdate.OrderDetailId != 0)
                    {
                        oldOrderQuantity = _orderDetailRepository.GetOrderDetailQuantity(detailToUpdate.OrderDetailId);
                    }
                    var detailReceived = order.OrderDetails[i];
                    detailToUpdate.Product = _productRepository.GetProduct(detailReceived.ProductId);
                    detailToUpdate.Product.UnitsInStock = (detailToUpdate.Product.UnitsInStock + oldOrderQuantity) - detailReceived.Quantity;
                    detailToUpdate.Product.OrderDetails = null;
                }
            }


            return _orderRepository.UpdateOrder(OrderToUpdate);

        }
    }
}
