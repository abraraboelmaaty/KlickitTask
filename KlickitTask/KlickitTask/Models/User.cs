namespace KlickitTask.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Governate { get; set; }
        public string City { get; set; }
        public string? Phone { get; set; }

    }
}
