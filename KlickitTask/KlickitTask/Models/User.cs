namespace KlickitTask.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Governate { get; set; }
        public string City { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
