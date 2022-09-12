using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Services.Interfaces
{
    public interface IProductServices
    {
        List<ProductDto> GetAllProducts();
        ProductDto? GetProduct(int productId);
        bool AddNewProduct(CreateProductDto product);
        bool UpdateProduct(ProductDto product);
        bool DeleteProduct(int ProductId);
    }
}
