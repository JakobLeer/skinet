using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public Repository(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> CountBySpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var count = await query.CountAsync();
            return count;
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<T> GetEntityBySpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            T entity = await query.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            var all = await _context.Set<T>().ToListAsync();
            return all;
        }

        public async Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var list = await query.ToListAsync();
            return list;
        }

        public IReadOnlyList<T> ListBySpec(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var list = query.ToList();
            return list;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            query = SpecificationEvaluator<T>.Evaluate(query, spec);
            return query;
        }
    }
}