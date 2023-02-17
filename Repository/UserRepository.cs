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

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
       



        public UserRepository ( UserManager<User> userManager, IMapper mapper) 
        {
          
            _userManager = userManager;
            _mapper =mapper;
        }



        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user,userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }


    }
}
