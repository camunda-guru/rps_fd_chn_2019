using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace bookstore.repository
{
    public class BookRepository : IBookRepository
    {
        private readonly MongoDbContext _context;
        public BookRepository(MongoDbContext context)
        {
            _context = context;
        }
         public async Task Create(Book book)
        {
            await _context.books.InsertOneAsync(book);
        }

        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(m => m.ISBN, id);
            DeleteResult deleteResult = await _context
                                                .books
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context
                            .books
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<Book> GetBook(long id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(m => m.ISBN, id);
            return _context
                    .books
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task<long> GetNextId()
        {
            return await _context.books.CountDocumentsAsync(new BsonDocument()) + 1;
        }

        public async Task<bool> Update(Book book)
        {
            ReplaceOneResult updateResult =
                await _context
                        .books
                        .ReplaceOneAsync(
                            filter: g => g.ISBN == book.ISBN,
                            replacement: book);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
