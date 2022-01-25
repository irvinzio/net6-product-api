using System.Net;
using System.Net.Http.Json;
using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Infrasttructure.Repositories;

namespace Tektok.Infrastructure.Repositories.MockApiRepo
{
    public class MockApiRepository: IBaseRepository<MockApiProductModel>, IMockApiRepository
    {
        static HttpClient client = new HttpClient();
        private readonly string _mockApiUrl = "https://61ef30a5d593d20017dbb369.mockapi.io/tekton/api/v1/product";
        public async Task<MockApiProductModel> Get(int id)
        {
            MockApiProductModel product = null;
            HttpResponseMessage response = await client.GetAsync($"{_mockApiUrl}/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<MockApiProductModel>();
            }
            return product;
        }

        public async Task<MockApiProductModel> Add(MockApiProductModel product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(_mockApiUrl, product);
            response.EnsureSuccessStatusCode();

            product = await response.Content.ReadAsAsync<MockApiProductModel>();
            return product;
        }
        public async Task<MockApiProductModel> Update(MockApiProductModel product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{_mockApiUrl}/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            product = await response.Content.ReadAsAsync<MockApiProductModel>();
            return product;
        }
    }
}
