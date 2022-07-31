using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KlickitTask.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("admin")]
        [JsonIgnore]
        public int? AdminId { get; set; }
        [JsonIgnore]
        public Admin? admin { get; set; }
        [JsonIgnore]
        public List<Product>? Products { get; set; }

    }
}
