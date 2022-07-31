using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KlickitTask.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderAddress { get; set; }
        [JsonIgnore]
        public double? TotalPrice { get { return products?.Sum(p => p.Price); } }
       
        [ForeignKey("customer")]
        [JsonIgnore]
        public int? CustomerId { get; set; }
        [JsonIgnore]
        public Customer? customer { get; set; }
        [JsonIgnore]
        public List<Product>? products { get; set; }
    }
}
