using BookStore.API.DTOs;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        public Task<List<Books>> GetAllBooksAsync();
        public Task<Books> GetBookByIdAsync(int bookId);
        public Task<int> AddBookAsync(ToAddDto toAddDto);
        public Task UpdateBookAsync(int bookId, ToAddDto toAddDto);
        public Task UpdateBookPatchAsync(int bookId, JsonPatchDocument toAddDto);
        public Task DeleteBookAsync(int bookId);


    }
}
