using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
