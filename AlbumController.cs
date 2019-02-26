using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using musicstore.Models;
using musicstore.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace musicstore.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _repo;
        public AlbumController(IAlbumRepository repo)
        {
            _repo = repo;
        }
        // GET api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> Get()
        {
            return new ObjectResult(await _repo.GetAllAlbums());
        }
        // GET api/todos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> Get(long id)
        {
            var album = await _repo.GetTodo(id);
            if (album == null)
                return new NotFoundResult();

            return new ObjectResult(album);
        }
        // POST api/todos
        [HttpPost]
        public async Task<ActionResult<Album>> Post([FromBody] Album album)
        {
            album.Id = await _repo.GetNextId();
            await _repo.Create(album);
            return new OkObjectResult(album);
        }
        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Album>> Put(long id, [FromBody] Album album)
        {
            var albumFromDb = await _repo.GetTodo(id);
            if (albumFromDb == null)
                return new NotFoundResult();
            album.Id = albumFromDb.Id;
            album.InternalId = albumFromDb.InternalId;
            await _repo.Update(album);
            return new OkObjectResult(album);
        }
        // DELETE api/todos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetTodo(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}
