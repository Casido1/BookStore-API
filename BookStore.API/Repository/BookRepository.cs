using BookStore.API.Data;
using BookStore.API.DTOs;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<Books>> GetAllBooksAsync()
        {
            var records = await _context.Books.ToListAsync();
            return records;
        }

        public async Task<Books> GetBookByIdAsync(int bookId)
        {
            var records = await _context.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();
            return records;
        }

        public async Task<int> AddBookAsync(ToAddDto toAddDto)
        {
            var book = new Books
            {
                title = toAddDto.title,
                Description = toAddDto.Description
            }; 
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(int bookId, ToAddDto toAddDto)
        {
            //var book = await _context.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();

            //if(book != null)
            //{
            //    book.title = toAddDto.title;
            //    book.Description = toAddDto.Description;
            //    await _context.SaveChangesAsync();
            //}

            //To avoid performance issues due to unnecessary multiple database calls, we'll use the code below

            var book = new Books()
            {
                Id = bookId,
                title = toAddDto.title,
                Description = toAddDto.Description
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument toAddDto)
        {
            var book = await _context.Books.FindAsync(bookId);

            if(book != null)
            {
                toAddDto.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            
        }


    }
}
