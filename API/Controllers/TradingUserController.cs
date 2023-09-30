using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.TradingUserDtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
        public async Task<IActionResult> GetTradingUsers()
        {
            return Ok(_mapper.Map<IList<TradingUserDto>>(await _unitOfWork.tradingUserRepository.GetTradingUsersAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTradingUserById(int id)
        {
            return Ok(_mapper.Map<TradingUserDto>(await _unitOfWork.tradingUserRepository.GetTradingUserByIdAsync(id)));
        }

        [HttpPost]
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
            return Ok(new ReturnedLoginTradingUserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
            // Assuming you want to return a successful response with the created user.
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginTradingUserDto loginDto)
        {
            var user = await _unitOfWork.userRepository.SingleOrDefaultAsync(user => user.UserName == loginDto.Username);
            // Retrieves a single user from the repository based on the provided username
            // The method is awaited since it is an asynchronous operation

            if (user == null) Unauthorized("User not exist");
            // This condition checks if the `user` variable is null.
            // If it is, then it means that the user does not exist.

            // However, the line `Unauthorized("User not exist");` is missing an assignment.
            // It should be changed to `return Unauthorized("User not exist");` in order to return
            // an Unauthorized result with the appropriate message if the user does not exist.


            using var hmac = new HMACSHA512(user.PasswordSalt);
            // Creates an instance of HMACSHA512 for hashing passwords
            // using the user's password salt value

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            // Computes the hash of the provided password by converting it to bytes
            // and passing it to the ComputeHash method of HMACSHA512

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.Password[i])
                    return Unauthorized("Invalid password");
                // Compares each byte of the computed hash with the corresponding byte
                // of the stored password hash. If any byte doesn't match, return an
                // Unauthorized result with a message indicating an invalid password.
            }

            return Ok(new ReturnedLoginTradingUserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            });
            // If the password verification succeeds, return an Ok result with the
            // authenticated user object.
        }

    }
}