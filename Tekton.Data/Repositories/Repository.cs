using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tekton.Data.Context;

namespace Tekton.Data.Repositories
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
