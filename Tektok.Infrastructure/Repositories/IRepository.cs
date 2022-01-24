﻿namespace Tekton.Infrasttructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
    }
}