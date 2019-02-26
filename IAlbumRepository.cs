using musicstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicstore.Repository
{
    public interface IAlbumRepository
    {
        // api/[GET]
        Task<IEnumerable<Album>> GetAllAlbums();
        // api/1/[GET]
        Task<Album> GetTodo(long id);
        // api/[POST]
        Task Create(Album album);
        // api/[PUT]
        Task<bool> Update(Album album);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}
