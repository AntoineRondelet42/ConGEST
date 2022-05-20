using AutoMapper;
using ConGEST.Models;
using ConGEST.DTOs;
using ConGEST.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConGEST.Controllers
{
[ApiController]
[Route("api/auth")]

    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IOptionsSnapshot<JwtSettings> _jwtSettings;

        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignUpDto userSignUpDto)
        {
            var user = _mapper.Map<User>(userSignUpDto);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpDto.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 400);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginDto.Email);

            if (user is null)
            {
                return NotFound("L'utilisateur n'existe pas.");
            }

            var userSignInResult = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            var roles = await _userManager.GetRolesAsync(user);

            if (userSignInResult)
            {
                return Ok(new
                {
                    Token = GenerateJwt(user, roles)
                });
            }
            return BadRequest("Mot de passe incorrect.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Le rôle doit avoir un nom");
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var RoleResult = await _roleManager.CreateAsync(newRole);

            if (RoleResult.Succeeded)
            {
                return Ok();
            }
            return Problem(RoleResult.Errors.First().Description, null, 500);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("user/{userEmail}/role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }

        [Authorize(Roles = "Admin,TEST")]
        [HttpGet("user")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }
        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + ' ' + user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var RoleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(RoleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.Value.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
