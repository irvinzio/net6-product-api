using System.Net;
using System.Net.Http.Json;
using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Infrasttructure.Repositories;

namespace Tektok.Infrastructure.Repositories.MockApiRepo
{
    public class MockApiRepository: IBaseRepository<MockApiProductModel>, IMockApiRepository
    {
        static HttpClient client = new HttpClient();
        public async Task<MockApiProductModel> Get(int id)
        {
            MockApiProductModel product = null;
            HttpResponseMessage response = await client.GetAsync("api/products/"+id.ToString());
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<MockApiProductModel>();
            }
            return product;
        }

        public async Task<MockApiProductModel> Add(MockApiProductModel product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();

            product = await response.Content.ReadAsAsync<MockApiProductModel>();
            return product;
        }
        public async Task<MockApiProductModel> Update(MockApiProductModel product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            product = await response.Content.ReadAsAsync<MockApiProductModel>();
            return product;
        }
    }
}
