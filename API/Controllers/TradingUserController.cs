using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.TradingUserDtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingProject.API.Controller;

namespace API.Controllers
{
    public class TradingUserController : APIV1ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public TradingUserController(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            this._tokenService = tokenService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTradingUsers([FromQuery] UserParams userParams)
        {
            var users = await _unitOfWork.tradingUserRepository.GetTradingUsersAsync(userParams);

            Response.AddPagenationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTradingUserById(int id)
        {
            return Ok(await _unitOfWork.tradingUserRepository.GetTradingUserByIdAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetTradingUserByUsername(string username)
        {
            return Ok(await _unitOfWork.tradingUserRepository.GetTradingUserByUsernameAsync(username));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> DeactiveTradingUserByUsername(DeactiveTradingUserByUsernameRequest user)
        {
            var result = await _unitOfWork.tradingUserRepository.DeactiveTradingUserByUsernameAsync(user.Username);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTradingUserByUsername(string username)
        {
            var tradingUser = _unitOfWork.tradingUserRepository.Find(x => x.Username == username).FirstOrDefault();
            if (tradingUser == null)
                return BadRequest("User not found");
            _unitOfWork.tradingUserRepository.Remove(tradingUser);
            await _unitOfWork.CompleteAsync();
            return Ok("Delete user successfuly");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateTradingUser(UpdateTradingUserDto tradingUserDto)
        {
            var username = tradingUserDto.Username;
            var tradingUser = _unitOfWork.tradingUserRepository.Find(x => x.Username == username).FirstOrDefault();
            if (tradingUser != null)
            {
                _mapper.Map(tradingUserDto, tradingUser);
                _unitOfWork.tradingUserRepository.Update(tradingUser);
                await _unitOfWork.CompleteAsync();
                return Ok(_mapper.Map<TradingUserDto>(tradingUser));
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterTradingUserDto registerDto)
        {
            if (await _unitOfWork.tradingUserRepository.UserExists(registerDto.Username)) return BadRequest("UserName is taken");
            using var sha512Encryptor = new HMACSHA512();

            var user = new TradingUser
            {
                Username = registerDto.Username.ToLower(),
                Name = registerDto.Name,
                Family = registerDto.Family,
                MobileNumber = registerDto.MobileNumber,
                Email = registerDto.Email,
                IsActive = true,
                Password = sha512Encryptor.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = sha512Encryptor.Key
            };

            await _unitOfWork.tradingUserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<TradingUserDto>(user));
            // Assuming you want to return a successful response with the created user.
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] LoginTradingUserDto loginDto)
        {
            var user = await _unitOfWork.tradingUserRepository.SingleOrDefaultAsync(user => user.Username == loginDto.Username);

            if (user == null) Unauthorized("User not exist");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.Password[i])
                    return Unauthorized("Invalid password");
            }

            return Ok(_mapper.Map<TradingUserDto>(user));
        }

    }
}