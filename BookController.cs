using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.models;
using bookstore.repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bookstore.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _repo;
        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }
        // GET api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return new ObjectResult(await _repo.GetAllBooks());
        }
        // GET api/book/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(long id)
        {
            var book = await _repo.GetBook(id);
            if (book == null)
                return new NotFoundResult();

            return new ObjectResult(book);
        }
        // POST api/book
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            book.ISBN =(int) await _repo.GetNextId();
            await _repo.Create(book);
            return new OkObjectResult(book);
        }
        // PUT api/book/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(long id, [FromBody] Book book)
        {
            var bookFromDb = await _repo.GetBook(id);
            if (bookFromDb == null)
                return new NotFoundResult();
            book.ISBN = bookFromDb.ISBN;
            book._Id = bookFromDb._Id;
            await _repo.Update(book);
            return new OkObjectResult(book);
        }
        // DELETE api/book/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetBook(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}
