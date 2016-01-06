using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public interface IBookRepository
    {
        void Add(Book book);
        IEnumerable<Book> GetAll();
        Book Find(string id);
        Book Remove(string id);
        void Update(Book book);
    }
}