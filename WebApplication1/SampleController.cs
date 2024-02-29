using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TestcontainersApi.Models;

namespace TestcontainersApi
{
    [Route("/api/test")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IMongoCollection<Entity> _collection;

        public SampleController(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase("testcontainers-api-db");
            _collection = db.GetCollection<Entity>("test-entities");
        }

        [HttpPost("{value}")]
        public async Task<IActionResult> Post(string value)
        {
            var entity = new Entity(value);

            await _collection.InsertOneAsync(entity);
            return new OkObjectResult($"{entity.Id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(p => p.Id, id);
            var entities = await _collection.FindAsync(filter);

            var entity = entities.FirstOrDefault();
            if (entities is null || entity is null)
                return new NotFoundResult();

            return new OkObjectResult(entity);
        }
    }
}
