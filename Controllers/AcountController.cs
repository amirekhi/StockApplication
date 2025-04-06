using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.DTOs.Action;
using MyWebApi.Intefaces;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        public readonly UserManager<AppUser> _userManager ;
        public readonly SignInManager<AppUser>  _signinManager ;
        public readonly ITokenService _tokenService;

        public AcountController(UserManager<AppUser> userManager , ITokenService tokenService , SignInManager<AppUser> signinManager)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }


        [HttpPost("register")]

        public async Task<IActionResult>  Register([FromBody] RegisterDto registerDto)
        {
            try{  
                 if(!ModelState.IsValid)
                     return BadRequest(ModelState);

                 var appUser = new AppUser{
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                 };

                 var createdUser = await _userManager.CreateAsync(appUser , registerDto.Password );

                 if(createdUser.Succeeded){
                    var roleResults = await _userManager.AddToRoleAsync( appUser , "User");
                    if(roleResults.Succeeded){
                        return Ok(
                            new NewUserDto{
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token =  _tokenService.CreateToken(appUser),
                            }
                        );
                    }else{
                        return StatusCode(500 , roleResults.Errors);
                    }
                 }else{
                    return StatusCode(500 , createdUser.Errors);
                 }

                 
            }catch( Exception e){
                return StatusCode(500, e);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login( LoginDto loginDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
            
            if(user == null ) return Unauthorized("Invalid Username ");


            var results = await _signinManager.CheckPasswordSignInAsync(user , loginDto.Password , false);


            if(!results.Succeeded) return Unauthorized("Invalid Username or Password");


            return Ok(
                new NewUserDto{
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),

                }
            );
        }
        
    }
}