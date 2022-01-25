using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tektok.Infrastructure.Repositories.MockApiRepo;
using Tekton.Data.Context;
using Tekton.Data.Entities;

namespace Tektok.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> InitDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

                var serviceProvider = scopedServices.ServiceProvider;
            var context = serviceProvider.GetRequiredService<TektonContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            await SeedProdcuts(context);

            return app;
        }
        private static async Task SeedProdcuts(TektonContext context)
        {
            var mockApiData = await new MockApiRepository().Get();

            if (!mockApiData.Any()) return;

            var productList = new List<Product>();
            foreach (var product in mockApiData)
            {
                productList.Add(new Product() { ProductMockId = product.Id, Detail = product.Description });
            }
            await context.Products.AddRangeAsync(productList);

           await context.SaveChangesAsync();
        }
    }
}
