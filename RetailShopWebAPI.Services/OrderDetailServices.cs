using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class OrderDetailServices: IOrderDetailServices
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailServices(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository
            ,IProductRepository productRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public bool CreateNewOrderDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            if(!_orderRepository.OrderExists(createOrderDetailDto.OrderId))
                throw new KeyNotFoundException("Order not found!");

            if (!_productRepository.ProductExists(createOrderDetailDto.ProductId))
                throw new KeyNotFoundException("Product not found!");

            var orderDetail = _mapper.Map<OrderDetail>(createOrderDetailDto);
            orderDetail.Product = _productRepository.GetProduct(createOrderDetailDto.ProductId);
            orderDetail.Product.OrderDetails = null;
            
            orderDetail.Order = _orderRepository.GetOrder(createOrderDetailDto.OrderId);
            orderDetail.Order.OrderDetails = null;
            orderDetail.Order.Customer = null;
            orderDetail.Order.Address = null;

            return _orderDetailRepository.CreateNewOrderDetail(orderDetail);
        }

        public bool DeleteOrderDetail(int orderDetailId)
        {
            if (!_orderDetailRepository.OrderDetailExists(orderDetailId))
                throw new KeyNotFoundException("Order Detail not found!");

            var orderDetail = _orderDetailRepository.GetOrderDetail(orderDetailId);
            return _orderDetailRepository.DeleteOrderDetail(orderDetail);
        }
    }
}
