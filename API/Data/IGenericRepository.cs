using System.Linq.Expressions;

namespace API.Data
{
    // Represents a generic repository interface for performing CRUD operations on entities.
    public interface IGenericRepository<T> where T : class
    {
        // Retrieves an entity by its ID synchronously.
        Task<T?> GetById(int id);

        // Retrieves an entity by its ID asynchronously.
        Task<T?> GetByIdAsync(int id);

        // Retrieves all entities of type T.
        Task<IEnumerable<T>> GetAllAsync();

        // Retrieves entities that match the specified expression.
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        // Retrieves entities that match the specified expression.
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression);

        // Adds a new entity to the repository.
        void Add(T entity);

        // Adds a new entity to the repository asynchronously.
        Task AddAsync(T entity);

        // Adds a collection of entities to the repository.
        void AddRange(IEnumerable<T> entities);

        // Adds a collection of entities to the repository asynchronously.
        Task AddRangeAsync(IEnumerable<T> entities);

        // Removes an entity from the repository.
        void Remove(T entity);

        // Removes a collection of entities from the repository.
        void RemoveRange(IEnumerable<T> entities);
    }
}