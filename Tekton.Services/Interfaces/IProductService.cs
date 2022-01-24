using System;
using System.Threading.Tasks;
using Tekton.Service.Dto;

namespace Tekton.Service.Interfaces
{
    public  interface IProductService
    {
        Task<ProductDto> Insert(ProductDto product);
        Task<ProductDto> Update(Guid id, ProductDto product);
        Task<ProductDto> Get(Guid id);
    }
}
