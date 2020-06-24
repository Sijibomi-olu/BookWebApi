using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Siji_BookWebApi.Dtos;
using Siji_BookWebApi.Entities;
using Siji_BookWebApi.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Siji_BookWebApi.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccount _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDtos registerUser)
        {
            ApplicationUser user = new ApplicationUser();

            user.FirstName = registerUser.FirstName;
            user.LastName = registerUser.LastName;
            user.UserName = registerUser.Username;
            user.Email = registerUser.Email;


            var newUser = await _account.CreateUser(user, registerUser.Password);
            if (newUser)
                return Ok(new { message = "User Created" });

            return BadRequest(new { message = "User not created" });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto userDto)
        {
            var user = await _account.SignIn(userDto);

            if (user == null || user.Username != userDto.Username)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _account.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(String id)
        {
            var user = await _account.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, [FromBody] ApplicationUser au)
        {
            au.Id = id;
            var updateAccount = await _account.Update(au);

            if (updateAccount)
            {
                return Ok("Account Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Account details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            var deleteAccount = await _account.Delete(id);
            if (deleteAccount)
            {
                return Ok("Account Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Account details" });
            }
        }
    }
}