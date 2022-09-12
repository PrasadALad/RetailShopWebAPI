using Microsoft.AspNetCore.Mvc;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Services.Interfaces;

namespace RetailShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController: Controller
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK,  Type = typeof(List<CustomerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCustomers()
        {
            List<CustomerDto> customers = new List<CustomerDto>();
            try
            {
                customers = _customerServices.GetAllCustomers();

                if (!ModelState.IsValid)
                    return BadRequest();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while getting customers data!");
                return BadRequest(ModelState);
            }
            return Ok(customers);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCustomer([FromQuery]int customerId)
        {
            CustomerDto customer = new CustomerDto();
            try
            {
                if(customerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id cannot be less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                customer = _customerServices.GetCustomer(customerId);

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
                ModelState.AddModelError("", "Error while getting customer data!");
                return BadRequest(ModelState);
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewCustomer([FromBody] CreateCustomerDto createCustomer)
        {
            try
            {
                if (createCustomer == null)
                    return BadRequest();

                if (createCustomer.Addresses == null || createCustomer.Addresses.Count <= 0)
                {
                    ModelState.AddModelError("", "Please add atleast one address for new customer!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (!_customerServices.CreateCustomer(createCustomer))
                {
                    ModelState.AddModelError("", "Error while adding new customer!");
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
                ModelState.AddModelError("", "Error while adding new customer!");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Saved!");
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCustomer([FromBody] CustomerDto customerToUpdate)
        {
            try
            {
                if (customerToUpdate == null)
                    return BadRequest();

                if(customerToUpdate.CustomerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id cannot be less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest();
                if (!_customerServices.UpdateCustomer(customerToUpdate))
                {
                    ModelState.AddModelError("", "Error while updating customer data!");
                    return BadRequest(ModelState);
                }

            }
            catch(KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while updating customer data!");
                return BadRequest(ModelState);
            }

            return Ok("Successfully Saved!");
        }

        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCustomer([FromQuery]int customerId)
        {
            try
            {
                if(customerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id cannot be less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest();

                if (!_customerServices.DeleteCustomer(customerId))
                {
                    ModelState.AddModelError("", "Error while deleting customer data!");
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
                ModelState.AddModelError("", "Error while deleting customer data!");
                return BadRequest(ModelState);
            }
            return Ok("Successfully deleted!");
        }
    }
}
