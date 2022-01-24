using System;
using System.Threading.Tasks;
using Tekton.Data.Entities;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Mapster;
using Tekton.Infrasttructure.Repositories;

namespace Tekton.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        public ProductService(IRepository<Product> productRepo) { _productRepo = productRepo; }
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

            product.Detail = productDto.Detail;
            product.Master = productDto.Master;

            return (await _productRepo.Update(product)).Adapt<ProductDto>();
        }
    }
}
