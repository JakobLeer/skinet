using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // Find repository to use
            string type = typeof(TEntity).Name;

            // Create and save it if not exists
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(
                                            repositoryType.MakeGenericType(typeof(TEntity)),
                                            _context);
                 _repositories.Add(type, repositoryInstance);
            }

            return (Repository<TEntity>) _repositories[type];
        }
    }
}