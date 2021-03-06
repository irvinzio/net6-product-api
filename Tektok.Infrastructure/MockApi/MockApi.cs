using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using Tektok.Infrastructure.MockApi;
using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Infrastructure.Interfaces;

namespace Tektok.Infrastructure.Repositories.MockApiRepo
{
    public class MockApi: IBaseRepository<MockApiProductModel>, IMockApiRepository
    {
        static HttpClient client = new HttpClient();
        private readonly string _mockApiUrl = "https://61ef30a5d593d20017dbb369.mockapi.io/tekton/api/v1/product";
        public async Task<List<MockApiProductModel>> Get()
        {
            List<MockApiProductModel> products = null;
            HttpResponseMessage response = await client.GetAsync(_mockApiUrl);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<List<MockApiProductModel>>();
            }
            return products;
        }
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
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            HttpResponseMessage response = await client.PostAsync(_mockApiUrl, product, formatter);
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
