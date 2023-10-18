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
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        // [AllowAnonymous]
        [Authorize]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = _unitOfWork.userRepository.Find(x => x.Id > 0).ToList();
            return users;
        }

        // [AllowAnonymous]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser?>> GetUser(int id)
        {
            var user = await _unitOfWork.userRepository.SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}