using System.Text.Json;
using TestcontainersApi.Models;

namespace TestcontainersTests
{
    public class SampleControllerTests : IClassFixture<TestFixture>
    {
        private HttpClient _client;
        public SampleControllerTests(TestFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Creates_and_gets_valid_entity()
        {
            var entityValue = "value";

            var postResponse = await _client.PostAsync($"/api/test/{entityValue}", null);
            postResponse.EnsureSuccessStatusCode();
            var id = await postResponse.Content.ReadAsStringAsync();

            var getResponse = await _client.GetAsync($"/api/test/{id}");
            var content = await getResponse.Content.ReadAsStringAsync();
            var entity = JsonSerializer.Deserialize<Entity>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            Assert.NotNull(entity);
            Assert.Equal(entityValue, entity.Value);
            Assert.Equal(id, entity.Id);
        }
    }
}