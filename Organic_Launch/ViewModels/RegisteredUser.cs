using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class RegisteredUser
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string UserRole { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
        ErrorMessage = "This is not a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Street Number")]
        public string StreetNum { get; set; }

        [Required]
        [DisplayName("Street Name")]
        public string StreetName { get; set; }

        [Required]
        [DisplayName("Province")]
        public string Province { get; set; }

        [Required]
        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [DisplayName("Farm Name")]
        public string FarmName { get; set; }

        [DisplayName("Farm Profile")]
        public string FarmProfile { get; set; }

    }
}
