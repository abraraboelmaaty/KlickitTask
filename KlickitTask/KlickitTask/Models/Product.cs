using System.Text.Json.Serialization;

namespace KlickitTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
        [JsonIgnore]
        public List<Category>? Categories { get; set; }

    }
}
