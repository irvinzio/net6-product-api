using Microsoft.EntityFrameworkCore;
using Tekton.Data.Context;
using Tekton.Infrastructure.Interfaces;

namespace Tekton.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly TektonContext context;
        public Repository(TektonContext context)
        {
            this.context = context;
        }

        public async Task<T> Get(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
