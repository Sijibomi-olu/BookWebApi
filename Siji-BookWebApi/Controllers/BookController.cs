using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siji_BookWebApi.Entities;
using Siji_BookWebApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers

{

    [Route("api/Book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBook _book;
        public BooksController(IBook book)
        {
            _book = book;
        }
        [HttpPost("AddedBook")]
        public void Post([FromBody] Book book)
        {
            _book.Add(book);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var createBook = await _book.AddAsync(book);

            if (createBook)
            {
                return Ok("Book Created");
            }
            else
            {
                return BadRequest(new { message = "Book details cannot be created" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var users = await _book.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _book.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            book.Id = id;
            var updateBook = await _book.Update(book);

            if (updateBook)
            {
                return Ok("Book Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Book details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteBook = await _book.Delete(id);
            if (deleteBook)
            {
                return Ok("Book Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Book details" });
            }
        }


    }
}