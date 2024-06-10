using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradingProject.API.Controller;

namespace API.Controllers
{
    public class AccountController : APIV1ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register(RegisterDto registerDto)
        //{
        //    var userExists = await userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName);
        //    if (!userExists) return BadRequest("UserName is taken");

        //    var user = new AppUser
        //    {
        //        UserName = registerDto.UserName.ToLowerInvariant(),
        //    };

        //    var result = await userManager.CreateAsync(user, registerDto.Password);

        //    if (!result.Succeeded) return BadRequest(result.Errors);

        //    var roleResult = await userManager.AddToRoleAsync(user, "Admin");

        //    if (!roleResult.Succeeded) return BadRequest(result.Errors);

        //    return Ok(new UserDto
        //    {
        //        Username = user.UserName,
        //        Token = await _tokenService.CreateToken(user)
        //    });
        //}

        [HttpPost]
        // [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username.ToLower());

            if (user == null) Unauthorized("Invalid Username");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return Ok(new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            });
        }

    }

}