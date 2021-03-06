using System;
using System.Threading.Tasks;
using Books.Api.Filters;
using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            this._booksRepository = booksRepository 
                    ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpGet]
        [BooksResultFilter]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntities = await _booksRepository.GetBooksAsync();
            return Ok(bookEntities);
        }

        [HttpGet]
        [BookResultFilter]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _booksRepository.GetBookAsync(id);
            if(bookEntity == null)
            {
                return NotFound();
            }

            return Ok(bookEntity);
        }
    }
}