using Microsoft.EntityFrameworkCore;
using RetailShopWebAPI.Common.Entities;
using RetailShopWebAPI.Repositories.Data;
using RetailShopWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopWebAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool AddNewProduct(Product product)
        {
            _dataContext.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            List<int> OrderDetailIds = _dataContext.OrderDetails.Where(od=>od.Product.ProductId==product.ProductId).Select(od=>od.OrderDetailId).ToList();
            List<int> orderIds = _dataContext.OrderDetails.AsNoTracking().Where(od => OrderDetailIds.Contains(od.OrderDetailId))
                .Select(od => od.Order.OrderId).ToList();

            List<Order> orders = _dataContext.Orders.AsNoTracking().Where(od => orderIds.Contains(od.OrderId))
                .Include(od => od.OrderDetails).ToList();
            orders = (from i in orders where i.OrderDetails.Count <=1 select i).ToList();

            if (orders != null && orders.Count > 0)
                _dataContext.RemoveRange(orders);
            
            _dataContext.Remove(product);
            return Save();
        }

        public List<Product> GetAllProducts()
        {
            return _dataContext.Products.AsNoTracking().OrderBy(p=> p.ProductId).ToList();
        }

        public Product? GetProduct(int productId)
        {
            return _dataContext.Products.AsNoTracking().Where(p => p.ProductId == productId).FirstOrDefault();
        }

        public bool ProductExists(int productId)
        {
            return _dataContext.Products.AsNoTracking().Any(p => p.ProductId == productId);
        }

        public bool UpdateProduct(Product product)
        {
            _dataContext.Update(product);
            return Save();
        }

        public bool Save()
        {
            return (_dataContext.SaveChanges() > 0);
        }

        public bool ProductAvailable(int productId, int orderQuantity, int currentQuantity = 0)
        {
            var product = _dataContext.Products.AsNoTracking().Where(p => p.ProductId == productId).FirstOrDefault();
            int inventoryAvailable = product.UnitsInStock + currentQuantity;
            return (inventoryAvailable >= orderQuantity);
        }

        public bool UpdateProducts(List<Product> products)
        {
            _dataContext.UpdateRange(products);
            return Save();
        }

       
    }
}
