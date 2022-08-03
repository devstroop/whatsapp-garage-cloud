using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class RegisterModel
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string? ConfirmPassword { get; set; }
    }
}
