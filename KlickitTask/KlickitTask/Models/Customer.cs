using System.Text.Json.Serialization;

namespace KlickitTask.Models
{
    public class Customer:User
    {
        [JsonIgnore]
        public List<Order>? orders { get; set; }
    }
}
