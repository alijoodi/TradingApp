using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace API.Data
{

    // Represents a generic repository for CRUD operations on entities of type T.
    // T must be a class.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // The application database context.
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        // Constructor that injects the AppDbContext dependency.
        public GenericRepository(DataContext context, IMapper mapper)
        {
            _context = context; _mapper = mapper;
        }

        // Adds a new entity to the database context.
        public void Add(T entity) => _context.Set<T>().Add(entity);

        // Asynchronously adds a new entity to the database context.
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        // Adds a range of entities to the database context.
        public void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);

        // Asynchronously adds a range of entities to the database context.
        public async Task AddRangeAsync(IEnumerable<T> entities) => await _context.Set<T>().AddRangeAsync(entities);

        // Retrieves a collection of entities that satisfy the specified expression.
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression);

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).SingleOrDefaultAsync();
        }

        // Retrieves all entities of type T from the database.

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        // Retrieves an entity of type T by its id.
        public async Task<T?> GetById(int id) => await _context.Set<T>().FindAsync(id);

        // Asynchronously retrieves an entity of type T by its id.
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        // Removes an entity from the database context.
        public void Remove(T entity) => _context.Set<T>().Remove(entity);

        // Removes a range of entities from the database context.
        public void RemoveRange(IEnumerable<T> entities) => _context.Set<T>().RemoveRange(entities);

        public void Update(T entity)=>_context.Set<T>().Update(entity);

    }
}