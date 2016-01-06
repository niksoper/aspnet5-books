using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MvcLibrary.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly ConcurrentDictionary<string, Book> books;
        private int nextId = 0;

        public BookRepository()
        {
            this.books = new ConcurrentDictionary<string, Book>();
            this.Add(new Book { Title = "RESTful API with MVC6", Author = "Nick Soper" });
        }

        public void Add(Book book)
        {
            if (book == null)
            {
                return;
            }

            this.nextId++;
            book.Id = nextId.ToString();

            this.books.TryAdd(book.Id, book);
        }

        public Book Find(string id)
        {
            Book book;
            this.books.TryGetValue(id, out book);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            return this.books.Values.OrderBy(b => b.Id);
        }

        public Book Remove(string id)
        {
            Book book;
            this.books.TryRemove(id, out book);
            return book;
        }

        public void Update(Book book)
        {
            this.books[book.Id] = book;
        }
    }
}
