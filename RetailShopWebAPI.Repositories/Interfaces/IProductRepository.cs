using RetailShopWebAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product? GetProduct(int productId);
        bool ProductExists(int productId);
        bool AddNewProduct(Product product);
        bool UpdateProduct(Product product);
        bool UpdateProducts(List<Product> products);
        bool DeleteProduct(Product product);
        bool ProductAvailable(int productId, int orderQuantity, int currentQuantity = 0);
        bool Save();
    }
}
