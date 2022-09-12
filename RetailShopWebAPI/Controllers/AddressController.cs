using Microsoft.AspNetCore.Mvc;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Services.Interfaces;

namespace RetailShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressServices _addressServices;

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AddressDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAddressesForCustomer([FromQuery]int customerId)
        {
            List<AddressDto> addresses = new List<AddressDto>();

            try
            {
                if (customerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                addresses = _addressServices.GetAllAddressesForCustomer(customerId);

                if(!ModelState.IsValid)
                    return BadRequest();
            }
            catch(KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error while getting addresses for a customer!");
                return BadRequest(ModelState);
            }

            return Ok(addresses);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAddress([FromQuery]int addressId)
        {
            AddressDto address = new AddressDto();
            try
            {
                if (addressId <= 0)
                {
                    ModelState.AddModelError("", "Address Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }
                address = _addressServices.GetAddress(addressId);

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
                ModelState.AddModelError("", "Error while getting address data!");
                return BadRequest(ModelState);
            }
            return Ok(address);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewAddress([FromBody]CreateAddressDto address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest(ModelState);
                }

                if(address.customerId <= 0)
                {
                    ModelState.AddModelError("", "Customer Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest();

                if (!_addressServices.AddNewAddress(address))
                {
                    ModelState.AddModelError("", "Error while adding new address!");
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
                ModelState.AddModelError("", "Error while adding new address!");
                return BadRequest(ModelState);
            }
            return Ok("Saved Successfully!");
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAddress([FromBody]AddressDto address)
        {
            try
            {
                if (address == null)
                    return BadRequest(ModelState);

                if(address.AddressId <= 0)
                {
                    ModelState.AddModelError("", "Address Id is less than or equal to zero!");
                }

                if (!ModelState.IsValid)
                    return BadRequest();

                if (!_addressServices.UpdateAddress(address))
                {
                    ModelState.AddModelError("", "Error while updating address data!");
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
                ModelState.AddModelError("", "Error while updating address data!");
                return BadRequest(ModelState);
            }
            return Ok("Saved Successfully!");
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAddress([FromQuery]int addressId)
        {
            try
            {
                if(addressId <= 0)
                {
                    ModelState.AddModelError("", "Address Id is less than or equal to zero!");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest();

                if (!_addressServices.DeleteAddress(addressId))
                {
                    ModelState.AddModelError("", "Error while deleting Address!");
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
                ModelState.AddModelError("", "Error while deleting Address!");
                return BadRequest(ModelState);
            }

            return Ok("Deleted Successfully");
        }

    }
}
