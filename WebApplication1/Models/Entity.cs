namespace TestcontainersApi.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public Entity(string value)
        {
            Id = Guid.NewGuid().ToString();
            Value = value;
        }
    }
}
