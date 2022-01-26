using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Infrastructure.Interfaces;

namespace Tektok.Infrastructure.MockApi
{

    public interface IMockApiRepository : IBaseRepository<MockApiProductModel> 
    {
        Task<MockApiProductModel> Get(int id);
        Task<List<MockApiProductModel>> Get();
    }
}
