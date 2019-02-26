using MongoDB.Bson;
using MongoDB.Driver;
using musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicstore.Repository
{
    public class AlbumRepository:IAlbumRepository
    {
        private readonly MongoContext _context;
        public AlbumRepository(MongoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            return await _context
                            .Todos
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Album> GetTodo(long id)
        {
            FilterDefinition<Album> filter = Builders<Album>.Filter.Eq(m => m.Id, id);
            return _context
                    .Todos
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Album album)
        {
            await _context.Todos.InsertOneAsync(album);
        }
        public async Task<bool> Update(Album album)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Todos
                        .ReplaceOneAsync(
                            filter: g => g.Id == album.Id,
                            replacement: album);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Album> filter = Builders<Album>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Todos
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
        }

    }
}
