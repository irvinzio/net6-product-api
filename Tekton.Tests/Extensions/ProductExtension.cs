using System;
using Tekton.Data.Entities;

namespace Tekton.Tests.Extensions
{
    public static class ProductExtension
    {
        public static Product GetProduct(Guid id)
        {
            return new Product()
            {
                Id = id,
                Detail = "test detail",
                ProductMockId = 1
            };
        }
    }
}
