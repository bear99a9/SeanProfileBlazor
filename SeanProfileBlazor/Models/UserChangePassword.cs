using System.ComponentModel.DataAnnotations;

namespace SeanProfileBlazor.Models
{
    public class UserChangePassword
    {
        public int UserId { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
