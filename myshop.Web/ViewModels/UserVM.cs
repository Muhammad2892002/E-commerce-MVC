using System.ComponentModel.DataAnnotations;

namespace myshop.Web.ViewModels
{
    public class UserVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required,MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
    ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required,MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#_@$!%*?&])[A-Za-z\d#_@$!%*?&]{6,}$",
    ErrorMessage = "Password must have uppercase, lowercase, number, and special character (#_@$!%*?&) with minimum 6 characters")]

        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match!")]


        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
    }
}
