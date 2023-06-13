using DataAccess.DBModels;
using DataAccess.IRepository;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        BookDBContext dbContext;
        public BookRepository(BookDBContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<List<Book>> GetBooksByShul(int shulId)
        {
            try
            {
                List<Book> books = new();
                books = await dbContext.Books.Where(b => b.ShulId == shulId).ToListAsync();
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetBooksByShul function " + ex.Message);
            }
        }
        public async Task<Author> GetAuthor(int? authorId, string? authorName)
        {
            try
            {
                if (authorId != null)
                    return await dbContext.Authors.Where(a => a.Id == authorId).FirstOrDefaultAsync();
                else if (authorName != null)
                    return await dbContext.Authors.Where(a => a.Name == authorName).FirstOrDefaultAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAuthor function " + ex.Message);
            }
        }
        public async Task<Category> GetCategory(int? categoryId, string? categoryName)
        {
            try
            {
                if (categoryId != null)
                    return await dbContext.Categories.Where(b => b.Id == categoryId).FirstOrDefaultAsync();
                else if (categoryName != null)
                {
                    Category c = await dbContext.Categories.Where(b => b.Name == categoryName).FirstOrDefaultAsync();
                    return c;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetCategory function " + ex.Message);
            }
        }
        public async Task<Edition> GetEdition(int? editionId, string? editionName)
        {
            try
            {
                if (editionId != null)
                    return await dbContext.Editions.Where(b => b.Id == editionId).FirstOrDefaultAsync();
                else if (editionName != null)
                    return await dbContext.Editions.Where(b => b.Name == editionName).FirstOrDefaultAsync();
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetBooksByEdition function " + ex.Message);
            }
        }

        public async Task DeleteBook(int bookId)
        {
            try
            {
                Book book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
                if (book != null)
                {
                    dbContext.Books.Remove(book);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteBook function " + ex.Message);
            }
        }

        public async Task UpdateBook(int bookId, Book b)
        {
            try
            {
                Book book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
                if (book != null)
                {
                    book.Name = b.Name;
                    book.VolumeNum = b.VolumeNum;
                    book.PublishYear = b.PublishYear;
                    book.ShulId = b.ShulId;
                    book.CategoryId = b.CategoryId;
                    book.AuthorId = b.AuthorId;
                    book.EditionId = b.EditionId;
                    book.Copies = b.Copies;
                    book.Description = b.Description;
                    book.BookImgName = b.BookImgName;
                    dbContext.Books.Update(book);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateBook function " + ex.Message);
            }
        }

        public async Task AddNewBook(Book book)
        {
            try
            {
                if (book != null)
                {
                    dbContext.Books.AddAsync(book);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddNewBook function " + ex.Message);
            }
        }

        public Task<List<Category>> GetCategories()
        {
            try
            {
                return dbContext.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetCategories function " + ex.Message);
            }
        }

        public Task<List<Author>> GetAuthors()
        {
            try
            {
                return dbContext.Authors.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAuthors function " + ex.Message);
            }
        }

        public Task<List<Edition>> GetEditions()
        {
            try
            {
                return dbContext.Editions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetEditions function " + ex.Message);
            }
        }

        public async Task<Author> AddAuthor(Author author)
        {
            try
            {

                if (author != null)
                {
                    dbContext.Authors.AddAsync(author);
                    await dbContext.SaveChangesAsync();
                }
                return author;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetEditions function " + ex.Message);
            }

        }

        public async Task<Category> AddCategory(Category category)
        {
            try
            {

                if (category != null)
                {
                    dbContext.Categories.AddAsync(category);
                    await dbContext.SaveChangesAsync();
                }
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetEditions function " + ex.Message);
            }
        }

        public async Task<Edition> AddEdition(Edition edition)
        {
            try
            {

                if (edition != null)
                {
                    dbContext.Editions.AddAsync(edition);
                    await dbContext.SaveChangesAsync();
                }
                return edition;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetEditions function " + ex.Message);
            }
        }

        public async Task UplaodExcel(IFormFile data)
        {

        }

        public async Task DeleteAuthor(int id)
        {
            try
            {
                IList<Book> bookList = dbContext.Books.ToList();

                foreach (Book item in bookList)
                {
                    if (item.AuthorId == id)
                    {
                        item.AuthorId = 6;
                        dbContext.Books.Update(item);
                        await dbContext.SaveChangesAsync();
                    }
                }

                Author author = await dbContext.Authors.FirstOrDefaultAsync(b => b.Id == id);
                if (author != null)
                {
                    dbContext.Authors.Remove(author);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteAuthor function " + ex.Message);
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                IList<Book> bookList = dbContext.Books.ToList();

                foreach (Book item in bookList)
                {
                    if (item.CategoryId == id)
                    {
                        item.CategoryId = 5;
                        dbContext.Books.Update(item);
                        await dbContext.SaveChangesAsync();
                    }
                }
                Category category = await dbContext.Categories.FindAsync(id);
                if (category != null)
                {
                    dbContext.Categories.Remove(category);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteAuthor function " + ex.Message);
            }
        }

        public async Task DeleteEdition(int id)
        {
            try
            {
                IList<Book> bookList = dbContext.Books.ToList();

                foreach (Book item in bookList)
                {
                    if (item.EditionId == id)
                    {
                        item.EditionId = 3;
                        dbContext.Books.Update(item);
                        await dbContext.SaveChangesAsync();
                    }
                }
                Edition edition = await dbContext.Editions.FirstOrDefaultAsync(b => b.Id == id);
                if (edition != null)
                {
                    dbContext.Editions.Remove(edition);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteAuthor function " + ex.Message);
            }
        }

        public async Task UpdateAuthor(int authorId, string author)
        {
            try
            {
                IList<Book> bookList = dbContext.Books.ToList();

                foreach (Book item in bookList)
                {
                    if (item.AuthorId == authorId)
                    {
                        item.Author.Name = author;
                        dbContext.Books.Update(item);
                        await dbContext.SaveChangesAsync();
                    }
                }

                Author myAuthor = await dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
                if (myAuthor != null)
                {
                    myAuthor.Name = author;
                    dbContext.Authors.Update(myAuthor);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateAuthor function " + ex.Message);
            }
        }

        public async Task UpdateEdition(int editionId, string newEdition)
        {
            try
            {
                IList<Book> bookList = dbContext.Books.ToList();

                foreach (Book item in bookList)
                {
                    if (item.EditionId == editionId)
                    {
                        item.Edition.Name = newEdition;
                        dbContext.Books.Update(item);
                        await dbContext.SaveChangesAsync();
                    }
                }

                Edition myEdition = await dbContext.Editions.FirstOrDefaultAsync(a => a.Id == editionId);
                if (myEdition != null)
                {
                    myEdition.Name = newEdition;
                    dbContext.Editions.Update(myEdition);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateAuthor function " + ex.Message);
            }
        }

        public async Task UpdateCategory(int categoryId, string newCategory)
        {
            try
            {
                IList<Book> bookList = dbContext.Books.ToList();

                foreach (Book item in bookList)
                {
                    if (item.CategoryId == categoryId)
                    {
                        item.Category.Name = newCategory;
                        dbContext.Books.Update(item);
                        await dbContext.SaveChangesAsync();
                    }
                }

                Category myCategory = await dbContext.Categories.FirstOrDefaultAsync(a => a.Id == categoryId);
                if (myCategory != null)
                {
                    myCategory.Name = newCategory;
                    dbContext.Categories.Update(myCategory);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateAuthor function " + ex.Message);
            }
        }

    }
}
