using Microsoft.AspNetCore.Mvc;
using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Common.Models;
using RetailShopWebAPI.Services.Interfaces;

namespace RetailShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllProducts()
        {
            List<ProductDto> products = new List<ProductDto>();
            try
            {
                products = _productServices.GetAllProducts();

                if (!ModelState.IsValid)
                    return BadRequest();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while retrieving products data!");
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct([FromQuery]int prodId)
        {
            var product = new ProductDto();
            try
            { 
                if (prodId <= 0)
                {
                    ModelState.AddModelError("", "Product Id cannot be zero or negative");
                    return BadRequest(ModelState);
                }

                product = _productServices.GetProduct(prodId);

                if(product == null)
                {
                    ModelState.AddModelError("", "Product not found!");
                    return NotFound(ModelState);
                }

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
                ModelState.AddModelError("", "Error while retrieving product data!");
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewProduct([FromBody]CreateProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (!_productServices.AddNewProduct(product))
                {
                    ModelState.AddModelError("", "Something went wrong while adding new product!");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while adding new product!");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Saved Successfully!");
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest(ModelState);
                }

                if(product.ProductId <= 0)
                {
                    ModelState.AddModelError("", "ProductId cannot be zero or negative");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (!_productServices.UpdateProduct(product))
                {
                    ModelState.AddModelError("", "Something went wrong while updating product!");
                    return StatusCode(500, ModelState);
                }
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while updating product!");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Saved Successfully!");
        }

        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProduct([FromQuery]int prodId)
        {
            try
            {
                if (prodId <= 0)
                {
                    ModelState.AddModelError("", "Product Id cannot be zero or negative");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (!_productServices.DeleteProduct(prodId))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting product!");
                    return StatusCode(500, ModelState);
                }
            }
            catch(KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while deleting product!");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok("Deleted Successfully!");
        }

    }
}
