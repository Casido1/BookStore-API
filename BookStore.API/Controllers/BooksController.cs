using BookStore.API.DTOs;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public BooksController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet("")]
        
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepo.GetAllBooksAsync();
            return Ok(books);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id)
        {
            var books = await _bookRepo.GetBookByIdAsync(id);
            return Ok(books);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody]ToAddDto toAddDto)
        {
            var id = await _bookRepo.AddBookAsync(toAddDto);
            return CreatedAtAction(nameof(GetBookById), new {id = id, controller = "books" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute]int id, [FromBody]ToAddDto toAddDto)
        {
            await _bookRepo.UpdateBookAsync(id, toAddDto);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument toAddDto)
        {
            await _bookRepo.UpdateBookPatchAsync(id, toAddDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id)
        {
            await _bookRepo.DeleteBookAsync(id);
            return Ok();
        }


    }
}
