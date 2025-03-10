﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> UserManager, ITokenRepository tokenRepository)
        {
            this.userManager = UserManager;
            this.tokenRepository = tokenRepository;
            
        }

    


        //POST:  /Api/Auth/
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            //usurario

            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! succesfully");
                    }
                }

               

            }

            return BadRequest("error ocurred when creating user");



        }

        //new log in method

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {

            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                //checjk password

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user

                    var roles = await userManager.GetRolesAsync(user);
                   

                    if(roles != null)
                    {
                        //create token

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        // map token with Dto Class

                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);

                    }




                }
            }

            return BadRequest("user name of password incorrect.");

        }

    }
}
