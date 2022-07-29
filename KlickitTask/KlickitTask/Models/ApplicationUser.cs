using Microsoft.AspNetCore.Identity;

namespace KlickitTask.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        //public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Governate { get; set; }
        public string City { get; set; }
    }
}
