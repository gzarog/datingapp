﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using datingapp.api.Data;
using datingapp.api.Dtos;
using datingapp.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace datingapp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
                              IConfiguration config,
                              IMapper mapper,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

         
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var results = await _userManager.CreateAsync(userToCreate ,userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if(results.Succeeded){
                return CreatedAtRoute("GetUser", new {controller ="Users", 
                                    id = userToCreate.Id }, userToReturn);

            }

          return BadRequest(results.Errors);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password,false);


            if(result.Succeeded){
                var appUser =_mapper.Map<UserForListDto>(user);
                return Ok(new
                {
                    token =  this.GenerateJWTToken(user),
                    user =appUser

                });
            }
          
            return Unauthorized();
           

        }

        private string  GenerateJWTToken(User user){
              var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

}