using System.Text.Json.Serialization;

namespace KlickitTask.Models
{
    public class Admin:User
    {
        [JsonIgnore]
        public List<Category>? Categories { get; set; }  
    }
}
