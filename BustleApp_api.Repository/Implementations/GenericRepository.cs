using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BustleApp_api.Repository.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BustleContext _context;

        protected GenericRepository(BustleContext context)
        {
            _context = context;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
