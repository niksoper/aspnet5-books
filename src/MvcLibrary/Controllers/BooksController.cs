using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using MvcLibrary.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcLibrary.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository books;

        public BooksController(IBookRepository books)
        {
            this.books = books;
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

            return this.CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Book book)
        {
            if (book.Id != id)
            {
                return this.HttpBadRequest();
            }

            if (this.books.Find(id) == null)
            {
                return this.HttpNotFound();
            }

            this.books.Update(book);
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
