namespace API.Data
{
    // Represents a unit of work for managing repositories and database operations.
    public interface IUnitOfWork
    {
        // Gets the repository specifically for all entity.
        IUserRepository userRepository { get; }
        ITradingUserRepository tradingUserRepository { get; }
        // Commits the changes made to the database and returns the number of affected rows.
        int Complete();

        // Commits the changes made to the database asynchronously and returns a task representing the operation.
        Task<int> CompleteAsync();
    }
}