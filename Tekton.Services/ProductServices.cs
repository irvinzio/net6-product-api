using System;
using System.Threading.Tasks;
using Tekton.Data.Entities;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Tekton.Service.Extensions;
using LazyCache;
using Tektok.Infrastructure.MockApi;
using Tekton.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Tekton.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IMockApiRepository _mockApiRepository;
        private readonly IAppCache _cache;
        private ILogger<ProductService> _logger { get; set; }

        public ProductService(IRepository<Product> productRepo, IMockApiRepository mockApiRepository, IAppCache cache) 
        {
            _productRepo = productRepo;
            _mockApiRepository = mockApiRepository;
            _cache = cache;
        }
        public async Task<ProductDto?> Get(Guid id)
        {
            _logger?.LogInformation($"getting local data for id {id}");
            var product = await _productRepo.Get(id);
            if (product == null) return null;
            _logger?.LogInformation($"getting external product data for id {product.ProductMockId}");
            var productsWithCaching = await _cache.GetOrAddAsync(product.Id.ToString(), () => _mockApiRepository.Get(product.ProductMockId));

            return product.ToDto(productsWithCaching);
        }

        public async Task<ProductDto> Insert(ProductAddDto productDto)
        {
            _logger?.LogInformation($"Inserting external product data for id {productDto}");
            var mockProduct = await _mockApiRepository.Add(productDto.ToMockModel());
            _logger?.LogInformation($"Insrted id {mockProduct.Id}");
            var product = await _productRepo.Add(productDto.ToProduct(mockProduct.Id));

            return product.ToDto(mockProduct);
        }

        public async Task<ProductDto?> Update(Guid id, ProductDto productDto)
        {
            _logger?.LogInformation($"Verify that product exist with id {id}");
            var product = await _productRepo.Get(id);

            if(product == null) { return null; }

            product.Detail = productDto.Detail;

            _logger?.LogInformation($"Updating local data of product id {id}");
            var updatedProduct = await _productRepo.Update(product);

            _logger?.LogInformation($"Updating external api data of product id {updatedProduct.ProductMockId}");

            var mockRepo = await _mockApiRepository.Update(updatedProduct.ToMockModel(productDto));

            return updatedProduct.ToDto(mockRepo);

        }
    }
}
