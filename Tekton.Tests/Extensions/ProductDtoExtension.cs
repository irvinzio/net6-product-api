using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Service.Dto;

namespace Tekton.Tests.Extensions
{
    public static class ProductDtoExtension
    {
        public static ProductDto GetProductDto(Guid id)
        {
            return new ProductDto()
            {
                Id = id,
                Name = "test name",
                Price = "500",
                Description = "test desc",
                Department = "test dep",
                Product = "test product",
                Detail = "test deatil",
            };
        }
        public static ProductAddDto GetProductAddDto()
        {
            return new ProductAddDto()
            {
                Name = "test name",
                Price = "500",
                Description = "test desc",
                Department = "test dep",
                Product = "test product",
                Detail = "test deatil",
            };
        }
    }
}
