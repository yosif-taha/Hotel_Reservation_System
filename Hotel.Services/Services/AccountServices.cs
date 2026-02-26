using Hotel.Domain.Entities;
using Hotel.Services.Dtos.Account;
using Hotel.Services.Dtos.Rooms;
using Hotel.Services.Interfaces;
using Hotel.Services.ResultPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Services
{
    public class AccountServices(UserManager<AppUser> _userManager, IConfiguration _configuration) : IAccountServices
    {
        public async Task<ResultT<UserResponseDto?>> LoginAsync(LoginRequestDto requestDto)
        {
            var user = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user == null) return ResultT<UserResponseDto?>.Failure(new Error(ErrorCode.NotFound, "Email Of This User IS Not Found !!"));
            var flag = await _userManager.CheckPasswordAsync(user, requestDto.Password);
            if (!flag) ResultT<UserResponseDto?>.Failure(new Error(ErrorCode.NotFound, "This User IS Not Found !!"));

            var result = new UserResponseDto()
            {

                Username = user.UserName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
            return ResultT<UserResponseDto?>.Success(result);
        }

        public async Task<ResultT<UserResponseDto?>> RegisterAsync(RegisterRequestDto requestDto)
        {
            var user = new AppUser()
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                UserName = requestDto.UserName,
                Email = requestDto.Email,
                PhoneNumber = requestDto.PhoneNumber,
            };


            var identityResult = await _userManager.CreateAsync(user, requestDto.Password);
            if (!identityResult.Succeeded) return ResultT<UserResponseDto?>.Failure(new Error(ErrorCode.BadRequest, "Invalid Operation when Create New User !!"));

            var result = new UserResponseDto()
            {
                Username = user.UserName,
                Email = user.Email,
                Token =  await GenerateTokenAsync(user)
            };
            return ResultT<UserResponseDto?>.Success(result);
        }



        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                myClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: myClaims,
                expires: DateTime.UtcNow.AddDays(_configuration.GetValue<double>("Jwt:ExpirationDays")),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
