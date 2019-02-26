using bookstore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.repository
{
    public interface IBookRepository
    {
        // api/[GET]
        Task<IEnumerable<Book>> GetAllBooks();
        // api/1/[GET]
        Task<Book> GetBook(long id);
        // api/[POST]
        Task Create(Book book);
        // api/[PUT]
        Task<bool> Update(Book book);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}
