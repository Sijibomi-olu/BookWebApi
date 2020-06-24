using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Siji_BookWebApi.Dtos;
using Siji_BookWebApi.Entities;
using Siji_BookWebApi.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Siji_BookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUser _userService;
        private IConfiguration _config;

        public UserController(IUser userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDtos userDto)
        {           
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username = userDto.Username,
                PhoneNo = userDto.PhoneNo
            };
            try
            {
                var userCreated = _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });



            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
           
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Expires = tokenDescriptor.Expires.ToString();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.FirstName,
                user.LastName,
                user.Email,
                access_token = tokenHandler.WriteToken(token),
                expires = Expires
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            user.Id = id;
            var updateUser = await _userService.Update(user);

            if (updateUser)
            {
                return Ok("User Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update User details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUser = await _userService.Delete(id);
            if (deleteUser)
            {
                return Ok("User Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete User details" });
            }
        }

    }
}