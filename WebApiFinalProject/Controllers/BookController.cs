using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.IService;
using BusinessLogic.Service;
using DataAccess.DBModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public List<BookDTO> books;
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
   // [Authorize]
    public class BookController : ControllerBase
    {
        IBookService bookService;
        //IUploadImageService uploadImageService;
        IMapper mapper;
        BookControllerSingleton b;
        public BookController(IBookService _bookService, IMapper _mapper)//, IUploadImageService _uploadImageService)
        {
            b = BookControllerSingleton.instance();
            bookService = _bookService;
            //uploadImageService = _uploadImageService;
            mapper = _mapper;
        }

        public string author { get; set; }


        ////////////////  Books functions  ///////////////////
        #region


        [HttpGet("ShulId")]
        
        //קבלת כל הספרים שך בית כנסת עם קוד בית כנסת מסוים
        public async Task<List<BookDTO>> GetBooksByShul([FromQuery] int shulId)
        {
            b.books = new();
            List<Book> l = new();
            l = await bookService.GetBooksByShul(shulId);
            foreach (var item in l)
            {
                BookDTO a = new BookDTO(item.Id, item.Name, (int)item.VolumeNum, GetAuthor((int)item.AuthorId, null).Result.Item2,
                    GetCategory((int)item.CategoryId, null).Result.Item2, GetEdition((int)item.EditionId, null).Result.Item2
                    , item.PublishYear, item.ShulId, item.Copies, item.Description, item.BookImgName, item.MaxCopies);
                b.books.Add(a);

            }
            return b.books;
        }   
        
        [HttpPut, Route("UpdateBook")]
        //עדכון פרטי ספר מסוים
        public Task UpdateBook([FromQuery] BookDTO book, IFormFile? data)
        {
            BookDTOInt bDTO = new BookDTOInt() { Id = book.Id, Name = book.Name, VolumeNum = book.VolumeNum,
                AuthorId = (string)book.Author!=null? GetAuthor(null, (string)book.Author).Result.Item1:6,
                CategoryId = (string)book.Category!=null? GetCategory(null, (string)book.Category).Result.Item1:5,
                EditionId = (string)book.Edition!=null?GetEdition(null, (string)book.Edition).Result.Item1:3,
                PublishYear = book.PublishYear, ShulId = book.ShulId,
                Copies = book.Copies, Description = book.Description,
                BookImgName = data != null ? UploadImageController.SaveBookImg(data).Result : book.BookImgName, MaxCopies = book.MaxCopies };
            Book b = mapper.Map<Book>(bDTO);
            return bookService.UpdateBook(book.Id, b);
        }
        //הוספת ספר חדש למאגר
        [HttpPost, Route("AddNewBook")]
        public Task AddNewBook([FromQuery] BookDTO Book, IFormFile? data)
        {
            BookDTOInt bDTO = new BookDTOInt() { Name = Book.Name, VolumeNum = Book.VolumeNum,
                AuthorId = Book.Author != null ? GetAuthor(null, (string)Book.Author).Result.Item1 : 6,
                CategoryId = Book.Category != null ? GetCategory(null, (string)Book.Category).Result.Item1 : 5, 
                EditionId = Book.Edition != null ? GetEdition(null, (string)Book.Edition).Result.Item1 : 3,
                PublishYear = Book.PublishYear, ShulId = Book.ShulId, Copies = Book.Copies, Description = Book.Description,
                BookImgName = data != null ? UploadImageController.SaveBookImg(data).Result : "", MaxCopies = Book.MaxCopies };
            Book b = mapper.Map<Book>(bDTO);
            return bookService.AddNewBook(b);
        }



        [HttpDelete, Route("DeleteBook")]
        public Task DeleteBook(int BookId)
        {
            return bookService.DeleteBook(BookId);
        }

        [HttpPost("UplaodExcel")]
        public async Task UplaodExcel([FromQuery] int shulId, IFormFile data)
        {
            BookDTO b = new();
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
                            b.Author = reader.GetValue(1).ToString();
                            b.Category = reader.GetValue(2).ToString();
                            b.Edition = reader.GetValue(3).ToString();
                            var strPublishYear = reader.GetValue(4).ToString();
                            b.PublishYear = Int32.Parse(strPublishYear);
                            var strVolumeNum = reader.GetValue(5).ToString();
                            b.VolumeNum = Int32.Parse(strVolumeNum);
                            var strCopies = reader.GetValue(5).ToString();
                            b.Copies = Int32.Parse(strCopies);
                            b.Description = reader.GetValue(5).ToString();
                            b.ShulId = shulId;
                            var maxCopies = reader.GetValue(5).ToString();
                            b.MaxCopies = Int32.Parse(maxCopies);
                            //BookDTO book = new BookDTO { Book = b, BookImg = null };
                            await AddNewBook(b, null);
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

        [HttpGet, Route("GetBooksByShulByName")]
        public async Task<List<BookDTO>> GetBooksByShulByName(string name)
        {
            return b.books.Where(b => b.Name.Contains(name)).ToList();
        }
        [HttpGet, Route("GetBooksByCategory")]
        public async Task<List<BookDTO>> GetBooksByShulByCategory(string category)
        {
            return b.books.Where(b => b.Category == category).ToList();
        }

        [HttpGet, Route("GetBooksByEdition")]
        public async Task<List<BookDTO>> GetBooksByShulByEdition(string edition)
        {
            return b.books.Where(b => b.Edition == edition).ToList();
        }
        [HttpGet, Route("GetBooksByShulByPublishingYear")]
        public async Task<List<BookDTO>> GetBooksByShulByPublishingYear(int publishingYear)
        {
            return b.books.Where(b => b.PublishYear == publishingYear).ToList();
        }
        [HttpGet, Route("GetBooksByShulByAuthor")]
        public async Task<List<BookDTO>> GetBooksByShulByAuthor(string author)
        {

            return b.books.Where(b => b.Author == author).ToList();
        }
        [HttpGet, Route("GetBooksBy")]
        public async Task<List<BookDTO>> GetBooksByShulBy(string value)
        {
            List<BookDTO> books = b.books.Where(b => b.Name.Contains(value) || b.Category.Contains(value) || b.Edition.Contains(value) || b.Author.Contains(value)).ToList();
            return books;
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
                AuthorDTO authorDTO = new() { Name = authorName };
                a = AddAuthor(authorDTO).Result;
            }
            return new Tuple<int, string>(a.Id, a.Name);
        }
        [HttpGet, Route("GetAuthors")]
        public async Task<List<AuthorDTO>> GetAuthors()
        {
            List<Author> l = new();
            l = await bookService.GetAuthors();
            List<AuthorDTO> a = mapper.Map<List<AuthorDTO>>(l);
            return a;
        }
        [HttpPost("AddAuthor")]
        public Task<Author> AddAuthor([FromQuery] AuthorDTO author)
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
                CategoryDTO categoryDTO = new() { Name = categoryName };
                c = AddCategory(categoryDTO).Result;
            }
            return new Tuple<int, string>(c.Id, c.Name);
        }
        [HttpGet, Route("GetCategories")]
        public async Task<List<CategoryDTO>> GetCategories()
        {
            List<Category> l = new();
            l = await bookService.GetCategories();
            List<CategoryDTO> c = mapper.Map<List<CategoryDTO>>(l);
            return c;
        }
        [HttpPost("AddCategory")]
        public Task<Category> AddCategory([FromQuery] CategoryDTO category)
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
                EditionDTO editionDTO = new() { Name = editionName };
                e = AddEdition(editionDTO).Result;
            }
            return new Tuple<int, string>(e.Id, e.Name);
        }
        [HttpGet, Route("GetEditions")]
        public async Task<List<EditionDTO>> GetEditions()
        {
            List<Edition> l = new();
            l = await bookService.GetEditions();
            List<EditionDTO> e = mapper.Map<List<EditionDTO>>(l);
            return e;
        }

        [HttpDelete, Route("DeleteEdition")]
        public Task DeleteEdition(string edition)
        {
            int editionId = GetEdition(null, edition).Result.Item1;
            return bookService.DeleteEdition(editionId);
        }
        [HttpPost("AddEdition")]
        public Task<Edition> AddEdition([FromQuery] EditionDTO edition)
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
