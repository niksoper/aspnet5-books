using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using MvcLibrary.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcLibrary.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository books;
        private readonly ILogger logger;

        public BooksController(IBookRepository books, ILogger<BooksController> logger)
        {
            this.books = books;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return this.books.GetAll();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetById(string id)
        {
            var book = this.books.Find(id);
            if (book == null)
            {
                return this.HttpNotFound();
            }

            return this.Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Book book)
        {
            if (book == null)
            {
                return this.HttpBadRequest();
            }

            this.books.Add(book);

            this.logger.LogVerbose("Added {0} by {1}", book.Title, book.Author);

            return this.CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Book book)
        {
            if (book.Id != id)
            {
                return this.HttpBadRequest();
            }

            var existingBook = this.books.Find(id);
            if (existingBook == null)
            {
                return this.HttpNotFound();
            }

            this.books.Update(book);

            this.logger.LogVerbose(
                "Updated {0} by {1} to {2} by {3}",
                existingBook.Title,
                existingBook.Author,
                book.Title,
                book.Author);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public NoContentResult Delete(string id)
        {
            this.books.Remove(id);
            return new NoContentResult();
        }
    }
}
