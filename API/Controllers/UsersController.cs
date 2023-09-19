using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradingProject.API.Controller;

namespace API.Controllers
{
    public class UsersController : APIV1ControllerBase
    {
        private readonly DataContext _context;


        public UsersController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        // [AllowAnonymous]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        // [AllowAnonymous]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser?>> GetUser(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }
    }
}