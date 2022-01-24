using System;
using System.Threading.Tasks;
using Tekton.Data.Entities;
using Tekton.Data.Repositories;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Mapster;

namespace Tekton.Service
{
    public class ProductServices : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        public ProductServices(IRepository<Product> productRepo) { _productRepo = productRepo; }
        public async Task<ProductDto?> Get(Guid id)
        {
            return  (await _productRepo.Get(id)).Adapt<ProductDto>();
        }

        public async Task<ProductDto> Insert(ProductAddDto productDto)
        {
            var product = productDto.Adapt<Product>();
            return (await _productRepo.Add(product)).Adapt<ProductDto>();
        }

        public async Task<ProductDto?> Update(Guid id, ProductDto productDto)
        {
            var product = await _productRepo.Get(id);
            if(product == null) { return null; }
            product = productDto.Adapt<Product>();
            return (await _productRepo.Update(product)).Adapt<ProductDto>();
        }
    }
}
