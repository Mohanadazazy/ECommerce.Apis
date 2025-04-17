using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly ECommerceDbContext _context;

        public GenericRepository(ECommerceDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trackChange ?
                    (IEnumerable<TEntity>)await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync()
                   : (IEnumerable<TEntity>)await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).AsNoTracking().ToListAsync();
            }
            return trackChange ?
                await _context.Set<TEntity>().ToListAsync() 
               :await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return  await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
                   
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}
