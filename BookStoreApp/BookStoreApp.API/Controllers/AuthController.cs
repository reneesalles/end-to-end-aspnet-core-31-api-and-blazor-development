using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.API.Data.DTOs.User;
using BookStoreApp.API.Data.Models;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userDto)
        {
            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Registration Model invalid - {nameof(Register)}", userDto);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                // every new user must have a "User" role
                await _userManager.AddToRoleAsync(user, "User");

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(Register)}", userDto);
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDto) {
            try {
                var user = await _userManager.FindByEmailAsync(userDto.Email);

                if (user == null) {
                    _logger.LogWarning($"Record not found - {nameof(Login)}", userDto);
                    return NotFound();
                }
                
                if (!await _userManager.CheckPasswordAsync(user, userDto.Password)) {
                    return NotFound();
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(Login)}", userDto);
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }
    }
}