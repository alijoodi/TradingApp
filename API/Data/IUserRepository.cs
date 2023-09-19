using API.Entities;

namespace API.Data
{
    // Creates an interface called IUserRepository which extends the IGenericRepository interface with the type parameter AppUser.
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        // Declares a method called UserExists that takes a string argument called userName and returns a Task<bool>.
        Task<bool> UserExists(string userName);
    }
}
