using System;
using System.Threading.Tasks;
using Tekton.Data.Entities;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Service.Extensions;
using Tekton.Infrastructure.Repositories;
using LazyCache;

namespace Tekton.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IMockApiRepository _mockApiRepository;
        private readonly IAppCache _cache;

        public ProductService(IRepository<Product> productRepo, IMockApiRepository mockApiRepository, IAppCache cache) 
        {
            _productRepo = productRepo;
            _mockApiRepository = mockApiRepository;
            _cache = cache;
        }
        public async Task<ProductDto?> Get(Guid id)
        {
            var product = await _productRepo.Get(id);
            if (product == null) return null;

            var productsWithCaching = await _cache.GetOrAddAsync(product.Id.ToString(), () => _mockApiRepository.Get(product.ProductMockId));

            return product.ToDto(productsWithCaching);
        }

        public async Task<ProductDto> Insert(ProductAddDto productDto)
        {
            var mockProduct = await _mockApiRepository.Add(productDto.ToMockModel());
            
            var product = await _productRepo.Add(productDto.ToProduct(mockProduct.Id));

            return product.ToDto(mockProduct);
        }

        public async Task<ProductDto?> Update(Guid id, ProductDto productDto)
        {
            var product = await _productRepo.Get(id);

            if(product == null) { return null; }

            product.Detail = productDto.Detail;

            var updatedProduct = await _productRepo.Update(product);

            var mockRepo = await _mockApiRepository.Update(updatedProduct.ToMockModel(productDto));

            return updatedProduct.ToDto(mockRepo);

        }
    }
}
