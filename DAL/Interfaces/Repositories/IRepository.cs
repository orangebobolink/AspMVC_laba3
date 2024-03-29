﻿using DAL.Entities;

namespace DAL.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> GetValueAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<bool> DeleteAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> CreateAsync(T entity);
    }
}
