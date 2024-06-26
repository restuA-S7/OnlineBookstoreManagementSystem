using BookstoreAPP.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPP.DAL
{
    public class BooksEF : IBook
    {
        private readonly AppDbContext _dbContext;

        public BooksEF(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Book Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetByBookTitle(string title)
        {
            var results = _dbContext.Books
                .Where(x => x.Title.Contains(title) || x.Author.Contains(title))
                .ToList();

            return results;

        }


        public Book GetById(int id)
        {
            var result = _dbContext.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (result == null)
            {
                throw new ArgumentException("Product Id Not Found");
            }
            return result;
        }

        public Book Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
