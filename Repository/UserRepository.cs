using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using static Shared.DataTransferObjects;
using System.Security.Claims;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private User? _user;
        private IConfiguration _configuration;

        public UserRepository ( UserManager<User> userManager, IMapper mapper, IConfiguration configuration) 
        {
          
            _userManager = userManager;
            _mapper =mapper;
            _configuration = configuration;
        }

        

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user,userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }


        public async Task<bool> ValidateUser(UserLoginDto userLogin)
        {
          _user = await _userManager.FindByNameAsync(userLogin.UserName);
          var result = (_user != null && await _userManager.CheckPasswordAsync(_user,userLogin.Password));
            if (!result)
                return result;
             return result;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = (Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
          {
             new Claim(ClaimTypes.Name, _user.UserName)
          };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }


        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
