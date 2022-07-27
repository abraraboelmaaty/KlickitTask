using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KlickitTask.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderAddress { get; set; }
        public double  TotalPrice { get; set; }
        [ForeignKey("customer")]
        [JsonIgnore]
        public int? CustomerId { get; set; }
        [JsonIgnore]
        public Customer? customer { get; set; }
        [JsonIgnore]
        public List<Product>? products { get; set; }
    }
}
