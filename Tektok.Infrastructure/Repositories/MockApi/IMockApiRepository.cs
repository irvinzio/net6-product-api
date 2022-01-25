using Tekton.Infrastructure.Repositories;

namespace Tektok.Infrastructure.Repositories.MockApi
{

    public interface IMockApiRepository : IBaseRepository<MockApiProductModel> 
    {
        Task<MockApiProductModel> Get(int id);
        Task<List<MockApiProductModel>> Get();
    }
}
