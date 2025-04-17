using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange);
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
