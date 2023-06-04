using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.IService;
using DataAccess.DBModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFinalProject.Controllers
{

    public class BookControllerSingleton : ControllerBase
    {
        public List<BookDto> books;
        private BookControllerSingleton()
        {


        }
        private static BookControllerSingleton _instance = null;

        public static BookControllerSingleton instance()
        {

            if (_instance == null)
                _instance = new BookControllerSingleton();
            return _instance;

        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookService bookService;
        IMapper mapper;
        BookControllerSingleton b;
        public BookController(IBookService _bookService, IMapper _mapper)
        {
            b = BookControllerSingleton.instance();
            bookService = _bookService;
            mapper = _mapper;
        }

        public string author { get; set; }


        ////////////////  Books functions  ///////////////////
        #region
    
        
        [HttpGet("ShulId")]
        public async Task<List<BookDto>> GetBooksByShul([FromQuery] int shulId)
        {
            b.books = new();
            List<Book> l = new();
            l = await bookService.GetBooksByShul(shulId);
            foreach (var item in l)
            {
                BookDto a = new BookDto(item.Id,item.Name, item.ChipId, item.VolumeNum, GetAuthor((int)item.AuthorId,null).Result.Item2, GetCategory((int)item.CategoryId,null).Result.Item2,GetEdition((int)item.EditionId,null).Result.Item2, item.PublishYear,item.ShulId,item.Copies,item.Description);
                b.books.Add(a); 
    
            }
            return b.books;
        }   
        [HttpGet, Route("GetBooksByShulByName")]  
        public async Task<List<BookDto>> GetBooksByShulByName(string name)
        {
            return b.books.Where(b => b.Name.Contains(name)).ToList();
        }
        [HttpGet, Route("GetBooksByCategory")]
        public async Task<List<BookDto>> GetBooksByShulByCategory(string category)
        {
            return b.books.Where(b => b.Category == category).ToList();
        }

        [HttpGet, Route("GetBooksByEdition")]
        public async Task<List<BookDto>> GetBooksByShulByEdition(string edition)
        {
            return b.books.Where(b => b.Edition == edition).ToList();
        }
        [HttpGet, Route("GetBooksByShulByPublishingYear")]
        public async Task<List<BookDto>> GetBooksByShulByPublishingYear(int publishingYear)
        {
            return b.books.Where(b => b.PublishYear == publishingYear).ToList();
        }
        [HttpGet, Route("GetBooksByShulByAuthor")]
        public async Task<List<BookDto>> GetBooksByShulByAuthor(string author)
        {

            return b.books.Where(b => b.Author == author).ToList();
        }
        [HttpGet, Route("GetBooksBy")]
        public async Task<List<BookDto>> GetBooksByShulBy(string value)
        {
            List<BookDto> books = b.books.Where(b => b.Name.Contains(value) || b.Category.Contains(value) || b.Edition.Contains(value) || b.Author.Contains(value)).ToList();
            return books;
        }

        [HttpPost, Route("AddNewBook")]
        public Task AddNewBook([FromBody] BookDto book)
        {  
            BookDTO2 bDTO = new BookDTO2() {Name=book.Name, ChipId= book.ChipId, VolumeNum= book.VolumeNum, AuthorId= book.Author != "" ? GetAuthor(null, (string)book.Author).Result.Item1:6, CategoryId= book.Category != "" ? GetCategory(null, (string)book.Category).Result.Item1:5, EditionId = book.Edition != "" ? GetEdition(null, (string)book.Edition).Result.Item1:3, PublishYear=book.PublishYear, ShulId= book.ShulId, Copies=book.Copies, Description=book.Description };
            Book b = mapper.Map<Book>(bDTO);
            return bookService.AddNewBook(b);
        }

        [HttpPut, Route("UpdateBook")]
        public Task UpdateBook(int bookId, [FromBody] BookDto book)
        {
            BookDTO2 bDTO = new BookDTO2() { Name = book.Name, ChipId = book.ChipId, VolumeNum = book.VolumeNum, AuthorId = GetAuthor(null, (string)book.Author).Result.Item1, CategoryId =  GetCategory(null, (string)book.Category).Result.Item1, EditionId = GetEdition(null, (string)book.Edition).Result.Item1, PublishYear = book.PublishYear, ShulId = book.ShulId, Copies = book.Copies, Description = book.Description };
            Book b = mapper.Map<Book>(bDTO);
            return bookService.UpdateBook(bookId, b);
        }

        [HttpDelete, Route("DeleteBook")]
        public Task DeleteBook(int BookId)
        {
            return bookService.DeleteBook(BookId);
        }
      
        [HttpPost("UplaodExcel")]
        public async Task UplaodExcel( [FromQuery] int shulId,IFormFile data)
        {
            BookDto b = new();
            try
            {
                if (data == null || data.Length <= 0)
                {
                    throw new Exception();
                }
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using var stream = data.OpenReadStream();
                using var reader = ExcelReaderFactory.CreateReader(stream);
                int sheetCount = 1;
                do
                {
                    while (reader.Read())
                    {
                        if (sheetCount != 1)
                        {
                            b.Name = reader.GetValue(0).ToString();
                            b.Author= reader.GetValue(1).ToString();
                            b.Category = reader.GetValue(2).ToString();
                            b.Edition = reader.GetValue(3).ToString();
                            var strPublishYear = reader.GetValue(4).ToString();
                            b.PublishYear = Int32.Parse(strPublishYear);
                            var strVolumeNum = reader.GetValue(5).ToString();
                            b.VolumeNum = Int32.Parse(strVolumeNum);
                            var strCopies = reader.GetValue(5).ToString();
                            b.Copies= Int32.Parse(strCopies);
                            b.Description = reader.GetValue(5).ToString();
                            b.ShulId = shulId;
                            b.ChipId = 0;
                            await AddNewBook(b);
                        }
                        sheetCount++;
                    }



                } while (reader.NextResult());
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetEditions function " + ex.Message);
            }
        }

        #endregion
        ////////////////  Authors functions  /////////////////
        #region
        [HttpGet("AuthorId")]
        public async Task<Tuple<int, string>> GetAuthor([FromQuery] int? authorId, string? authorName)
        {
            Author a = await bookService.GetAuthor(authorId, authorName);
            if (a == null)
            {
                AuthorDto authorDto = new() { Name = authorName };
                a = AddAuthor(authorDto).Result;
            }
            return new Tuple<int, string>(a.Id, a.Name);
        }
        [HttpGet, Route("GetAuthors")]
        public async Task<List<AuthorDto>> GetAuthors()
        {
            List<Author> l = new();
            l = await bookService.GetAuthors();
            List<AuthorDto> a = mapper.Map<List<AuthorDto>>(l);
            return a;
        }
        [HttpPost("AddAuthor")]
        public Task<Author> AddAuthor([FromQuery] AuthorDto author)
        {
            Author a = mapper.Map<Author>(author);
            return bookService.AddAuthor(a);
        }
        [HttpDelete, Route("DeleteAuthor")]
        public Task DeleteAuthor(string author)
        {
            int authorId = GetAuthor(null, author).Result.Item1;


            return bookService.DeleteAuthor(authorId);
        }
        [HttpPut, Route("UpdateAuthor")]
        public Task UpdateAuthor(string author, string newAuthor)
        {
            int authorId = GetAuthor(null, author).Result.Item1;
            return bookService.UpdateAuthor(authorId, newAuthor);
        }
        #endregion
        ////////////////  Categories functions  ///////////////
        #region
        [HttpGet("CategoryIdOrName")]
        public async Task<Tuple<int, string>> GetCategory([FromQuery] int? categoryId, string? categoryName)
        {
            Category c = await bookService.GetCategory(categoryId, categoryName);
            if (c == null)
            {
                CategoryDto categoryDto = new() { Name = categoryName };
                c = AddCategory(categoryDto).Result;
            }
            return new Tuple<int, string>(c.Id, c.Name);
        }
        [HttpGet, Route("GetCategories")]
        public async Task<List<CategoryDto>> GetCategories()
        {
            List<Category> l = new();
            l = await bookService.GetCategories();
            List<CategoryDto> c = mapper.Map<List<CategoryDto>>(l);
            return c;
        }
        [HttpPost("AddCategory")]
        public Task<Category> AddCategory([FromQuery] CategoryDto category)
        {
            Category c = mapper.Map<Category>(category);
            return bookService.AddCategory(c);
        }
        [HttpDelete, Route("DeleteCategory")]
        public Task DeleteCategory(string category)
        {
            int categoryId = GetCategory(null, category).Result.Item1;
            return bookService.DeleteCategory(categoryId);
        }

        [HttpPut, Route("UpdateCategory")]
        public Task UpdateCategory(string category, string newCategory)
        {
            int categoryId = GetCategory(null, category).Result.Item1;
            return bookService.UpdateCategory(categoryId, newCategory);
        }
        #endregion
        ////////////////  Editions functions  ////////////////
        #region
        [HttpGet("EditionId")]
        public async Task<Tuple<int, string>> GetEdition([FromQuery] int? editionId, string? editionName)
        {
            Edition e = await bookService.GetEdition(editionId, editionName);
            if (e == null)
            {
                EditionDto editionDto = new() { Name = editionName };
                e = AddEdition(editionDto).Result;
            }
            return new Tuple<int, string>(e.Id, e.Name);
        }
        [HttpGet, Route("GetEditions")]
        public async Task<List<EditionDto>> GetEditions()
        {
            List<Edition> l = new();
            l = await bookService.GetEditions();
            List<EditionDto> e = mapper.Map<List<EditionDto>>(l);
            return e;
        }

        [HttpDelete, Route("DeleteEdition")]
        public Task DeleteEdition(string edition)
        {
            int editionId = GetEdition(null, edition).Result.Item1;
            return bookService.DeleteEdition(editionId);
        }
        [HttpPost("AddEdition")]
        public Task<Edition> AddEdition([FromQuery] EditionDto edition)
        {
            Edition e = mapper.Map<Edition>(edition);
            return bookService.AddEdition(e);
        }
        [HttpPut, Route("UpdateEdition")]
        public Task UpdateEdition(string edition, string newEdition)
        {
            int editionId = GetEdition(null, edition).Result.Item1;
            return bookService.UpdateEdition(editionId, newEdition);
        }
        #endregion

    }
}
