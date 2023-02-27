using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/userAuthentication")]
    [ApiController]
    public class AuthenticationController :ControllerBase
    {
        public IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        public AuthenticationController(IRepositoryManager repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }



        [HttpPost("RegisterUser") ]
        public async Task <IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _repository.userRepository.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] UserLoginDto user)
        {
            if (!await _repository.userRepository.ValidateUser(user))
                return Unauthorized();
            return Ok(
            new{ Token = await _repository.userRepository.CreateToken()});
        }
    }
}
