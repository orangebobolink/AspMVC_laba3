﻿using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Repositories
{
    internal abstract class MainRepository<T>
         : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _dbContext;

        public MainRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();


            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var existingOrder = _dbContext.Set<T>().SingleOrDefault(o => o.Id == entity.Id);
            if (existingOrder != null)
                _dbContext.Entry(existingOrder).State = EntityState.Detached;

            _dbContext.Attach(entity);
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<List<T>> GetAllAsync()
            => await _dbContext.Set<T>()
                                .ToListAsync();

        public async Task<T> GetValueAsync(int id)
            => await _dbContext.Set<T>()
                                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var existingOrder = _dbContext.Set<T>().SingleOrDefault(o => o.Id == entity.Id);
            if (existingOrder != null)
                _dbContext.Entry(existingOrder).State = EntityState.Detached;

            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }
    }
}
