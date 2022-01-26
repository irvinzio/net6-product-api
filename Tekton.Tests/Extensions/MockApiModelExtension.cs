using Tektok.Infrastructure.Repositories.MockApi;

namespace Tekton.Tests.Extensions
{
    public static class MockApiModelExtension
    {
        public static MockApiProductModel GetMockApiModel()
        {
            return new MockApiProductModel()
            {
                Department = "dep",
                Description = "desc",
                Name = "name",
                Id = 1,
                Price = "500",
                Product = "prod"
            };
        }
    }
}
