using System.ComponentModel.DataAnnotations;

namespace dotnet_rpg.Models.Dto.User
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
    }
}