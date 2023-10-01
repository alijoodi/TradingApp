using AutoMapper;

namespace API.Data
{
    // Represents a unit of work for performing database operations.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        // Gets the repository for accessing AppUser.
        public IUserRepository userRepository { get; private set; }
        public ITradingUserRepository tradingUserRepository { get; private set; }

        // Constructor that injects the AppDbContext dependency and initializes the AppUser repository.

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            userRepository = new UserRepository(_context, mapper);
            tradingUserRepository = new TradingUserRepository(_context, mapper);
        }

        // Saves changes made in the database context and returns the number of affected rows.
        public int Complete() => _context.SaveChanges();

        // Asynchronously saves changes made in the database context and returns the number of affected rows.
        public Task<int> CompleteAsync() => _context.SaveChangesAsync();

        // Disposes the instance of the AppDbContext.
        public void Dispose() => _context.Dispose();
    }
}