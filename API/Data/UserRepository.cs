using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        // Initializes a new instance of the 'PlatformRepository' class.
        // It takes an AppDbContext as a parameter, which is used to access the underlying database context.
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> UserExists(string userName)
        {
            // Ensure that the `userName` parameter is not null or empty.
            ArgumentException.ThrowIfNullOrEmpty(userName);

            // Check if there is any user in the `_context.Users` collection
            // whose `UserName` property matches the provided `userName`
            // (ignoring case sensitivity).
            return await _context.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }

    }
}