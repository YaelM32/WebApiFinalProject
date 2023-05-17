using BusinessLogic.Dto;
using DataAccess.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IService
{
    public interface IBookService
    {
        public Task<List<Book>> GetBooksByShul(int shulId);
        public Task<Author> GetAuthor(int? authorId, string? authorName);
        public Task<Category> GetCategory(int? categoryId, string? categoryName);
        public Task<Edition> GetEdition(int? editionId, string? editionName);
        public Task DeleteBook(int BookId);
        public Task UpdateBook(int bookId,Book b);
        public Task AddNewBook(Book b);
        public  Task<List<Category>> GetCategories();
        public  Task<List<Author>> GetAuthors();
        public  Task<List<Edition>> GetEditions();
        public Task<Author> AddAuthor(Author author);
        public Task<Category> AddCategory(Category categoryName);
        public Task<Edition> AddEdition(Edition editionName);
        public Task UplaodExcel(IFormFile file);

        public Task DeleteAuthor(int id); 
        public Task DeleteCategory(int id);
        public Task DeleteEdition(int id);

    }
}
