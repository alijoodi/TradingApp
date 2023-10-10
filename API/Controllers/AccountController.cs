using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using TradingProject.API.Controller;

namespace API.Controllers
{
    public class AccountController : APIV1ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AccountController(IUnitOfWork unitOfWork ,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (await _unitOfWork.userRepository.UserExists(registerDto.UserName)) return BadRequest("UserName is taken");

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
            };

            await _unitOfWork.userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return Ok(new UserDto{
                Username=user.UserName,
                Token=_tokenService.CreateToken(user)
            }); 
        }

        [HttpPost] // Attribute specifying that this method handles HTTP POST requests
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.userRepository.SingleOrDefaultAsync(user => user.UserName == loginDto.Username);
            
            if (user == null) Unauthorized("User not exist");

            return Ok(new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            });
        }
    
    }

}