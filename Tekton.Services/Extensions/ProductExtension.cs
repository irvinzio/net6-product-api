using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Data.Entities;
using Tekton.Service.Dto;

namespace Tekton.Service.Extensions
{
    //todo: use mappper to create the object mapping
    public static class ProductExtension
    {
        public static ProductDto ToDto(this Product product, MockApiProductModel mockProduct)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Detail = product.Detail,
                Name = mockProduct.Name,
                Price = mockProduct.Price,
                Description = mockProduct.Description,
                Department = mockProduct.Department,
                Product = mockProduct.Product
            };
        }
        public static MockApiProductModel ToMockModel(this ProductAddDto product)
        {
            return new MockApiProductModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Department = product.Department,
                Product = product.Product
            };
        }
        public static MockApiProductModel ToMockModel(this ProductDto product)
        {
            return new MockApiProductModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Department = product.Department,
                Product = product.Product
            };
        }
        public static Product ToProduct(this ProductAddDto product, int mockProdcutId)
        {
            return new Product()
            {
                Detail = product.Detail,
                ProductMockId = mockProdcutId
            };
        }

        
    }
}
