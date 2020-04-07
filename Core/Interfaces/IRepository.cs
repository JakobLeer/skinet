using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetEntityByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityBySpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec);
    }
}