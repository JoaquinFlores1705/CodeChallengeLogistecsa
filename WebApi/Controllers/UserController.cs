using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IGenericSecurityRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, IGenericSecurityRepository<User> userRepository, IMapper mapper, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Name = user.Name,
                Lastname = user.Lastname,
                Admin = user.Role == UserRole.Administrator
            };
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetUsers()
        {

            var users = await _userRepository.GetAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDto>>(users));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            return _mapper.Map<User, UserDto>(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(User user)
        {
            user.UserName = user.Email;
            IdentityResult result = await _userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.ToArray()[0].Description);
            }

            return _mapper.Map<User, UserDto>(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(string id, User user)
        {
            user.Id = id;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("No se actualizo el usuario");
            }

            return Ok(user);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("No se encontro el usuario");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo eliminar"));
            }

            return Ok(user);
        }
    }
}
