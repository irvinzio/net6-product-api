using System;
using System.Threading.Tasks;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;

namespace Tekton.Service
{
    public class ProductServices : IProductService
    {
        public Task<ProductDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> Insert(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> Update(Guid id, ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
