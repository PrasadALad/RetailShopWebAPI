using AutoMapper;
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
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServices(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public bool AddNewProduct(CreateProductDto product)
        {
            return _productRepository.AddNewProduct(_mapper.Map<Product>(product));
        }

        public bool DeleteProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                throw new KeyNotFoundException("Product not found!");
            }
            var productToDelete = _productRepository.GetProduct(productId);
            return _productRepository.DeleteProduct(productToDelete);
        }

        public List<ProductDto> GetAllProducts()
        {
            return _mapper.Map<List<ProductDto>>(_productRepository.GetAllProducts());
        }

        public ProductDto? GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                throw new KeyNotFoundException("Product not found!");
            }
            return _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));
        }

        public bool UpdateProduct(ProductDto product)
        {
            if (!_productRepository.ProductExists(product.ProductId))
            {
                throw new KeyNotFoundException("Product not found!");
            }
            return _productRepository.UpdateProduct(_mapper.Map<Product>(product));
        }
    }
}
