namespace KlickitTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public List<Order> Orders { get; set; }
        public List<Category> Categories { get; set; }

    }
}
