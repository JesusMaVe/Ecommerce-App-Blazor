using EcommerceApi.Data.Repositories;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceApi.Data.UnitOfWork;

 public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceDbContext _context;
        private IDbContextTransaction? _transaction;
        
        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Products = new GenericRepository<Product>(_context);
            Categories = new GenericRepository<Category>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderItems = new GenericRepository<OrderItem>(_context);
            CartItems = new GenericRepository<CartItem>(_context);
        }
        
        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<OrderItem> OrderItems { get; private set; }
        public IGenericRepository<CartItem> CartItems { get; private set; }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }