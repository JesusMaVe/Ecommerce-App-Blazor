using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EcommerceDbContext _context;
        protected readonly DbSet<T> _dbSet;
        
        public GenericRepository(EcommerceDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
        
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
        
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }
        
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        
        public async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null)
        {
            return expression == null 
                ? await _dbSet.CountAsync() 
                : await _dbSet.CountAsync(expression);
        }
        
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
    }