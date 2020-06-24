using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Siji_BookWebApi.Entities;
using Siji_BookWebApi.Interface;

namespace Siji_BookWebApi.Controllers
{
    [Route("api/Characteristics")]
    [ApiController]
    public class CharacteristicsController : Controller
    {
        private ICharacteristics _characteristics;
        public CharacteristicsController(ICharacteristics characteristics)
        {
            _characteristics = characteristics;
        }

        [HttpPost]
        public void Post([FromBody] Characteristics characteristics)
        {
            _characteristics.Add(characteristics);
        }

        [HttpPost("AddCharacteristics")]
        public async Task<IActionResult> AddCharacteristics([FromBody] Characteristics characteristics)
        {
            var createCharacteristics = await _characteristics.AddAsync(characteristics);

            if (createCharacteristics)
            {
                return Ok("Characteristics Created");
            }
            else
            {
                return BadRequest(new { message = "Unable to create Characteristics details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _characteristics.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _characteristics.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Characteristics characteristics)
        {
            characteristics.Id = id;
            var updateCharacteristics = await _characteristics.Update(characteristics);

            if (updateCharacteristics)
            {
                return Ok("Characteristics Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Characteristics details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteCharacteristics = await _characteristics.Delete(id);
            if (deleteCharacteristics)
            {
                return Ok("Characteristics Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Characteristics details" });
            }
        }
    }
}
