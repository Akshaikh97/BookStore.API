using BookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var records = await _context.Books.Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).ToListAsync();
            //return records;
            var records = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(records);
        }
        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            //var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).FirstOrDefaultAsync();
            //return records;
            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);
        }
        //public async Task<int> AddBookAsync(BookModel bookModel)
        //{
        //    var book = new Books()
        //    {
        //        Title = bookModel.Title,
        //        Description = bookModel.Description
        //    };
        //    _context.Books.Add(book);
        //    await _context.SaveChangesAsync();
        //    return book.Id;
        //}

        public async Task<BookModel> AddBookAsync(BookModel bookModel)
        {
            var Books = new Books();
            _mapper.Map(bookModel, Books);
            _context.Books.Add(Books);
            await _context.SaveChangesAsync();
            _mapper.Map(Books, bookModel);
            return bookModel;
        }
        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.Title = bookModel.Title;
                book.Description = bookModel.Description;
                await _context.SaveChangesAsync();
            }
        }
        //public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        //{
        //    var book = await _context.Books.FindAsync(bookId);
        //    _mapper.Map(book, bookModel);
        //    //if (book != null)
        //    //{
        //    //    book.Title = bookModel.Title;
        //    //    book.Description = bookModel.Description;
        //    //    await _context.SaveChangesAsync();
        //    //}
        //}
        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books() { Id = bookId };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
