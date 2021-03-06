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
            var count = await query.CountAsync().ConfigureAwait(false);
            return count;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
            return item;
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            T entity = await query.FirstOrDefaultAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            var all = await _context.Set<T>().ToListAsync().ConfigureAwait(false);
            return all;
        }

        public async Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var list = await query.ToListAsync().ConfigureAwait(false);
            return list;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            return SpecificationEvaluator<T>.Evaluate(query, spec);
        }
    }
}