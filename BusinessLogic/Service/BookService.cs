using BusinessLogic.Dto;
using BusinessLogic.IService;
using DataAccess.DBModels;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class BookService : IBookService
    {
        IBookRepository bookRepository;

        public BookService(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }

        public Task<List<Book>> GetBooksByShul(int shulId)
        {
            return bookRepository.GetBooksByShul(shulId);
        }
        public Task<Author> GetAuthor(int? authorId, string? authorName)
        {
            return bookRepository.GetAuthor(authorId, authorName);
        }

        public Task<Category> GetCategory(int? categoryId, string? categoryName)
        {
            return bookRepository.GetCategory(categoryId, categoryName);
        }
        public Task<Edition> GetEdition(int? editionId, string? editionName)
        {
            return bookRepository.GetEdition(editionId, editionName);
        }

        public Task DeleteBook(int bookId)
        {
            return bookRepository.DeleteBook(bookId);
        }

        public Task UpdateBook(int bookId, Book b)
        {
            return bookRepository.UpdateBook(bookId, b);
        }

        public Task AddNewBook(Book b)
        {
            return bookRepository.AddNewBook(b);

        }

        public Task<List<Category>> GetCategories()
        {
            return bookRepository.GetCategories();
        }

        public Task<List<Author>> GetAuthors()
        {
            return bookRepository.GetAuthors();
        }

        public Task<List<Edition>> GetEditions()
        {
            return bookRepository.GetEditions();
        }

        public Task<Author> AddAuthor(Author author)
        {
            return bookRepository.AddAuthor(author);

        }

        public Task<Category> AddCategory(Category category)
        {
            return bookRepository.AddCategory(category);

        }

        public Task<Edition> AddEdition(Edition edition)
        {
            return bookRepository.AddEdition(edition);

        }

        public Task UplaodExcel(IFormFile file)
        {
            return bookRepository.UplaodExcel(file);
        }
    }
}
