using Microsoft.AspNetCore.Mvc;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Services.Interfaces;

namespace RetailShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailServices _orderDetailServices;

        public OrderDetailController(IOrderDetailServices orderDetailServices)
        {
            _orderDetailServices = orderDetailServices;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewOrderDetail([FromBody]CreateOrderDetailDto orderDetail)
        {
            try
            {
                if (orderDetail == null)
                    return BadRequest(ModelState);

                if(orderDetail.OrderId<=0)
                {
                    ModelState.AddModelError("", "Order id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if (orderDetail.ProductId <= 0)
                {
                    ModelState.AddModelError("", "Product id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if (!_orderDetailServices.CreateNewOrderDetail(orderDetail))
                {
                    ModelState.AddModelError("", "Error while adding order detail data!");
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
                ModelState.AddModelError("", "Error while adding order detail data!");
                return BadRequest(ModelState);
            }
            return Ok("Successfully saved!");
        }

        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrderDetail([FromQuery]int orderDetailId)
        {
            try
            {
                if(orderDetailId <= 0)
                {
                    ModelState.AddModelError("", "OrderDetailId is less than or equal to zero!");
                    return BadRequest(ModelState);
                }    

                if(!ModelState.IsValid)
                    return BadRequest();

                if (!_orderDetailServices.DeleteOrderDetail(orderDetailId))
                {
                    ModelState.AddModelError("", "Error while deleting the order detail!");
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
                ModelState.AddModelError("", "Error while deleting the order detail!");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Deleted!");
        }
    }
}
