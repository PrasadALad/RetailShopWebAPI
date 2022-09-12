using Microsoft.AspNetCore.Mvc;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Controllers;
using RetailShopWebAPI.Services.Interfaces;

namespace RetailShopWebAPI.Tests.RetailShopWebAPI.Controllers
{
    public class OrderControllerTests
    {
        private readonly IOrderServices _orderServices;
        public OrderControllerTests()
        {
            _orderServices = A.Fake<IOrderServices>();
        }

        [Theory]
        [InlineData(1)]
        public void GetOrder_PossitiveOrderId_ReturnsOk(int orderId)
        {
            //Assign
            var order = A.Fake<OrderDto>();
            A.CallTo(() => _orderServices.GetOrder(orderId)).Returns(order);
            var controller = new OrderController(_orderServices);

            //Act
            var result = controller.GetOrder(orderId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Should().NotBeOfType(typeof(BadRequestObjectResult));
            result.Should().NotBeOfType(typeof(NotFoundObjectResult));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetOrder_ZeroOrLessThanZeroOrderId_ReturnsBadRequest(int orderId)
        {
            //Assign
            var order = A.Fake<OrderDto>();
            A.CallTo(() => _orderServices.GetOrder(orderId)).Returns(order);
            var controller = new OrderController(_orderServices);

            //Act
            var result = controller.GetOrder(orderId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.Should().NotBeOfType(typeof(OkObjectResult));
            result.Should().NotBeOfType(typeof(NotFoundObjectResult));
        }

        [Fact]
        public void AddNewOrder_OneOrderDetailWithPositiveAddressAndCustomerId_ReturnsOk()
        {
            //Assign
            var orderToCreate = A.Fake<CreateOrderDto>();
            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;
            A.CallTo(() => _orderServices.CreateNewOrder(orderToCreate)).Returns(true);
            var controller = new OrderController(_orderServices);

            //Act
            var result = controller.AddNewOrder(orderToCreate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Should().NotBeOfType(typeof(BadRequestObjectResult));
            result.Should().NotBeOfType(typeof(NotFoundObjectResult));
        }

        [Theory]
        [InlineData(-1,1)]
        [InlineData(-1,-1)]
        [InlineData(1,-1)]
        [InlineData(0,0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, 0)]
        [InlineData(0,-1)]
        public void AddNewOrder_ZeroOrNegativeAddressIdAndCustomerId_ReturnsBadRequest(int customerId, int addressId)
        {
            //Assign
            var orderToCreate = A.Fake<CreateOrderDto>();
            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.OrderDetails.Add(orderDetail);
            orderToCreate.CustomerId = customerId;
            orderToCreate.AddressId = addressId;
            A.CallTo(() => _orderServices.CreateNewOrder(orderToCreate)).Returns(true);
            var controller = new OrderController(_orderServices);

            //Act
            var result = controller.AddNewOrder(orderToCreate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.Should().NotBeOfType(typeof(OkObjectResult));
            result.Should().NotBeOfType(typeof(NotFoundObjectResult));
        }

        [Fact]
        public void AddNewOrder_NullOrderDetailsList_ReturnsBadRequest()
        {
            //Assign
            var orderToCreate = A.Fake<CreateOrderDto>();
            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderToCreate.OrderDetails = null;
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;
            A.CallTo(() => _orderServices.CreateNewOrder(orderToCreate)).Returns(true);
            var controller = new OrderController(_orderServices);

            //Act
            var result = controller.AddNewOrder(orderToCreate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.Should().NotBeOfType(typeof(OkObjectResult));
            result.Should().NotBeOfType(typeof(NotFoundObjectResult));
        }

        [Fact]
        public void AddNewOrder_EmptyOrderDetailsList_ReturnsBadRequest()
        {
            //Assign
            var orderToCreate = A.Fake<CreateOrderDto>();
            var orderDetail = A.Fake<CreateOrderOrderDetailDto>();
            orderToCreate.OrderDetails = A.Fake<List<CreateOrderOrderDetailDto>>();
            orderToCreate.CustomerId = 1;
            orderToCreate.AddressId = 1;
            A.CallTo(() => _orderServices.CreateNewOrder(orderToCreate)).Returns(true);
            var controller = new OrderController(_orderServices);

            //Act
            var result = controller.AddNewOrder(orderToCreate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.Should().NotBeOfType(typeof(OkObjectResult));
            result.Should().NotBeOfType(typeof(NotFoundObjectResult));
        }

    }
}
