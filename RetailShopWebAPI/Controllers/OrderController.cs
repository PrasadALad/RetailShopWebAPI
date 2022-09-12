using Microsoft.AspNetCore.Mvc;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Services.Interfaces;

namespace RetailShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllOrdersForACustomer([FromQuery]int customerId)
        {
            List<OrderDto> orders = new List<OrderDto>();
            try
            {
                if(customerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                orders = _orderServices.GetAllOrdersForACustomer(customerId);
                
                if (!ModelState.IsValid)
                    return BadRequest();
            }
            catch(KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while fetching orders for a customer!");
                return BadRequest(ModelState);
            }

            return Ok(orders);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrder([FromQuery]int orderId)
        {
            OrderDto order = new OrderDto();
            try
            {
                if(orderId <= 0)
                {
                    ModelState.AddModelError("", "Order Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }
                order = _orderServices.GetOrder(orderId);

                if (!ModelState.IsValid)
                    return BadRequest();
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while getting order detail!");
                return BadRequest(ModelState);
            }
            return Ok(order);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewOrder([FromBody]CreateOrderDto order)
        {
            try
            {
                if(order == null)
                    return BadRequest(ModelState);

                if (order.CustomerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if(order.AddressId <= 0)
                {
                    ModelState.AddModelError("", "Address Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if(order.OrderDetails == null || order.OrderDetails.Count <= 0)
                {
                    ModelState.AddModelError("", "Please add atleast one order detail/product!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest();

                if(!_orderServices.CreateNewOrder(order))
                {
                    ModelState.AddModelError("", "Error while saving new order!");
                    return BadRequest(ModelState);
                }

            }
            catch(ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while saving new order!");
                return BadRequest(ModelState);
            }
            return Ok("Successfully Saved!");
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateOrder([FromBody]UpdateOrderDto order)
        {
            try
            {
                if (order == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest();

                if (!_orderServices.UpdateOrder(order))
                {
                    ModelState.AddModelError("", "Error while updating order!");
                    return BadRequest(ModelState);
                }

            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while updating order!");
                return BadRequest(ModelState);
            }
            return Ok("Successfully Saved!");
        }

        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrder([FromQuery]int orderId)
        {
            try
            {
                if(orderId<=0)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest();
                if (!_orderServices.DeleteOrder(orderId))
                {
                    ModelState.AddModelError("", "Error while deleting order!");
                    return BadRequest(ModelState);
                }
            }
            catch(KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while deleting order!");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Deleted!");
        }
    }
}
