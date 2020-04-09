using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets the entity with the id given.
        /// </summary>
        /// <returns>An entity or null if none exists with the id given</returns>
        Task<T> GetEntityByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        /// <summary>
        /// Gets the entity matching the criteria defined in the spec.
        /// </summary>
        /// <param name="spec">A specification with criteria.</param>
        /// <returns>The first entity that matches the criteria in the sspecification, null if none match.</returns>
        Task<T> GetEntityBySpecAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec);

        Task<int> CountBySpecAsync(ISpecification<T> spec);
    }
}