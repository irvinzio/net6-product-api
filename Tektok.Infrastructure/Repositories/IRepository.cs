namespace Tekton.Infrasttructure.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
    }
    public interface IRepository<T> : IBaseRepository<T> where T : class
    {
        Task<T> Get(Guid id);
    }
}
