using BookStore.API.DTOs;
using BookStore.API.Models;
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

namespace BookStore.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _SignInMgr;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<AppUser> userMgr, SignInManager<AppUser> SignInMgr, IConfiguration configuration)
        {
            _userMgr = userMgr;
            _SignInMgr = SignInMgr;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email       

            };

            return await _userMgr.CreateAsync(user, registerDto.Password);

        }

        public async Task<string> LoginAsync(Login login)
        {
            var result = await _SignInMgr.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWTKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT : ValidIssuer"],
                audience: _configuration["JWT : ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
