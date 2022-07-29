using System.ComponentModel.DataAnnotations;

namespace KlickitTask.DTO
{
    public class RegisterModel
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Governate { get; set; }
        public string City { get; set; }
    
    }
}
